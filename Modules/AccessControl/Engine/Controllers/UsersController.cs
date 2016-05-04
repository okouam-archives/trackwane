using System;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Contracts.Models;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig config;

        public UsersController(IExecutionEngine executionEngine, IPlatformConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [HttpGet, Route("token")]
        public string GetAccessToken(string email, string password)
        {
           return "Bearer " + executionEngine.Query<GetAccessToken>(AppKeyFromHeader).Execute(email, password);
        }
        
        [Secured, HttpGet, Route("users/{userKey}")]
        public UserDetails FindById(string userKey)
        {
            return executionEngine.Query<FindByKey>(AppKeyFromHeader).Execute(userKey);
        }

        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}")]
        public void ArchiveUser(string organizationKey, string userKey)
        {
            executionEngine.Handle(new ArchiveUser(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, Administrators, HttpGet, Route("organizations/{organizationKey}/users/count")]
        public int CountInOrganization(string organizationKey)
        {
            return executionEngine.Query<CountInOrganization>(AppKeyFromHeader, organizationKey).Execute();
        }


        [Secured, SystemManagers, HttpGet, Route("users/count")]
        public int CountInSystem()
        {
            return executionEngine.Query<CountInSystem>(AppKeyFromHeader).Execute();
        }

        [Secured, AdministratorsOrUser, HttpPost, Route("organizations/{organizationKey}/users/{userKey}")]
        public void UpdateUser(string organizationKey, string userKey, UpdateUserModel model)
        {
            executionEngine.Handle(new UpdateUser(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey)
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Password = model.Password
            });
        }

        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users")]
        public string RegisterUser(string organizationKey, RegisterApplicationModel model)
        {
            var cmd = new RegisterUser(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, model.UserKey, model.DisplayName, model.Email, model.Password);
            cmd.UserKey = cmd.UserKey ?? new Hashids(config.Get("secret-key")).EncodeLong(DateTime.Now.Ticks);
            executionEngine.Handle(cmd);
            return cmd.UserKey;
        }
    }
}
