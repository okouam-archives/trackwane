using System;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.AccessControl.Models;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class UserApiController : BaseApiController
    {
        private const string RESOURCE_URL = "organizations/{organizationKey}/users/{userKey}";
        private const string COLLECTION_URL = "organizations/{organizationKey}/users";
        private readonly IExecutionEngine executionEngine;
        private readonly IConfig config;

        public UserApiController(IExecutionEngine executionEngine, IConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [HttpGet, Route("token")]
        public string GetAccessToken(string email, string password) =>
            "Bearer " + executionEngine.Query<GetAccessToken>().Execute(email, password);
        
        [HttpPost, Route("root")]
        public string CreateRootUser(RegisterUserModel model)
        {
            var cmd = new CreateRootUser
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Password = model.Password,
                UserKey = new Hashids(config.GetPlatformKey("secret-key")).EncodeLong(DateTime.Now.Ticks)
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
        public string RegisterUser(string organizationKey, RegisterUserModel model)
        {
            var cmd = new RegisterUser(CurrentClaims.UserId, organizationKey, model.UserKey, model.DisplayName, model.Email, model.Password);
            cmd.UserKey = cmd.UserKey ?? new Hashids(config.GetPlatformKey("secret-key")).EncodeLong(DateTime.Now.Ticks);
            executionEngine.Send(cmd);
            return cmd.UserKey;
        }
    }
}
