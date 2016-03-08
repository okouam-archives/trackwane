﻿using System.Web.Http;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.AccessControl.Models.Users;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class UserApiController : BaseApiController
    {
        private const string RESOURCE_URL = "organizations/{organizationKey}/users/{userKey}";
        private const string COLLECTION_URL = "organizations/{organizationKey}/users";
        private readonly IExecutionEngine executionEngine;

        public UserApiController(IExecutionEngine executionEngine)
        {
            this.executionEngine = executionEngine;
        }
    
        [HttpGet, Route("users")]
        public UserDetails FindByCredentials(string email, string password) =>
            executionEngine.Query<FindByCredentials>().Execute(email, password);
        
        [HttpPost, Route("root")]
        public string CreateRootUser(RegisterUserModel model)
        {
            var cmd = new CreateRootUser
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Password = model.Password
            };

            executionEngine.Send(cmd);

            return cmd.UserKey;
        }

        [Secured, HttpGet, Route("users/{userKey}")]
        public UserDetails FindById(string userKey) =>
            executionEngine.Query<FindByKey>().Execute(userKey);
        
        [Secured, Administrators, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveUser(string organizationKey, string userKey) =>
            executionEngine.Send(new ArchiveUser(CurrentClaims.UserId, organizationKey, userKey));
        
        [Secured, AdministratorsOrUser, HttpPost, Route(RESOURCE_URL)]
        public void UpdateUser(string organizationKey, string userKey, UpdateUserModel model) =>
            executionEngine.Send(new UpdateUser(CurrentClaims.UserId, organizationKey, userKey)
            {
                DisplayName = model.DisplayName, Email = model.Email, Password = model.Password
            });
        
        [Secured, Administrators, HttpPost, Route(COLLECTION_URL)]
        public void RegisterUser(string organizationKey, RegisterUserModel model) =>
            executionEngine.Send(new RegisterUser(CurrentClaims.UserId, organizationKey, model.UserKey, model.DisplayName, model.Email, model.Password));
        
    }
}
