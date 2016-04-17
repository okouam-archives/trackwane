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
    public class UserApiController : BaseApiController
    {
        private const string RESOURCE_URL = "organizations/{organizationKey}/users/{userKey}";
        private const string COLLECTION_URL = "organizations/{organizationKey}/users";
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig config;

        public UserApiController(IExecutionEngine executionEngine, IPlatformConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [HttpGet, Route("token")]
        public string GetAccessToken(string email, string password)
        {
           return "Bearer " + executionEngine.Query<GetAccessToken>(CurrentClaims.ApplicationKey).Execute(email, password);
        }
        
        [HttpPost, Route("root")]
        public string CreateRootUser(CreateApplicationModel model)
        {
            var cmd = new RegisterApplication(model.ApplicationKey ?? new Hashids(config.Get("secret-key")).EncodeLong(DateTime.Now.Ticks))
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Password = model.Password,
                UserKey = new Hashids(config.Get("secret-key")).EncodeLong(DateTime.Now.Ticks)
            };

            executionEngine.Send(cmd);

            return cmd.UserKey;
        }

        [Secured, HttpGet, Route("users/{userKey}")]
        public UserDetails FindById(string userKey)
        {
            return executionEngine.Query<FindByKey>(CurrentClaims.ApplicationKey).Execute(userKey);
        }

        [Secured, Administrators, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveUser(string organizationKey, string userKey)
        {
            executionEngine.Send(new ArchiveUser(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, AdministratorsOrUser, HttpPost, Route(RESOURCE_URL)]
        public void UpdateUser(string organizationKey, string userKey, UpdateUserModel model)
        {
            executionEngine.Send(new UpdateUser(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, userKey)
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Password = model.Password
            });
        }

        [Secured, Administrators, HttpPost, Route(COLLECTION_URL)]
        public string RegisterUser(string organizationKey, CreateApplicationModel model)
        {
            var cmd = new RegisterUser(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, model.UserKey, model.DisplayName, model.Email, model.Password);
            cmd.UserKey = cmd.UserKey ?? new Hashids(config.Get("secret-key")).EncodeLong(DateTime.Now.Ticks);
            executionEngine.Send(cmd);
            return cmd.UserKey;
        }
    }
}
