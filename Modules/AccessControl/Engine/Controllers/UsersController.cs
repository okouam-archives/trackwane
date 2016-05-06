using System;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Contracts.Contracts;
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
        public IHttpActionResult GetAccessToken(string email, string password)
        {
            var token = executionEngine.Query<GetAccessToken>(AppKeyFromHeader).Execute(email, password);

            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("The credentials provided were not recognized");
            }

            return Ok("Bearer " + token);
        }
        
        [Secured, HttpGet, Route("users/{userKey}")]
        public UserDetailsResponse FindById(string userKey)
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
        public IHttpActionResult UpdateUser(string organizationKey, string userKey, UpdateUserRequest request)
        {
            if (ModelState.IsValid)
            {
                executionEngine.Handle(new UpdateUser(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey)
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    Password = request.Password
                });
            }

            return BadRequest(ModelState);
        }

        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users")]
        public IHttpActionResult RegisterUser(string organizationKey, RegisterUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var userKey = new Hashids(config.Get("secret-key")).EncodeLong(DateTime.Now.Ticks);
                var cmd = new RegisterUser(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey, request.DisplayName, request.Email, request.Password);
                executionEngine.Handle(cmd);
                return Created(Request.RequestUri.Host + "/users/" +  userKey, userKey);
            }

            return BadRequest(ModelState);

        }
    }
}
