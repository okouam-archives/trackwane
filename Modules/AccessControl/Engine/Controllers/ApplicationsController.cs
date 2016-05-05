using System;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Contracts.Contracts;
using Trackwane.AccessControl.Engine.Commands.Application;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class ApplicationsController : BaseApiController
    {
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig config;

        public ApplicationsController(IExecutionEngine executionEngine, IPlatformConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [HttpPost, Route("application")]
        public IHttpActionResult RegisterApplication(RegisterApplicationRequest request)
        {
            if (ModelState.IsValid)
            {
                var applicationExists = executionEngine.Query<CountInApplication>(AppKeyFromHeader).Execute() > 0;

                if (applicationExists)
                {
                    return BadRequest($"The application with key {AppKeyFromHeader} already exists");
                }

                if (config.SecretKey != request.SecretKey)
                {
                    return BadRequest("The platform secret key provided is invalid");
                }

                var cmd = new RegisterApplication(AppKeyFromHeader)
                {
                    Email = request.Email,
                    DisplayName = request.DisplayName,
                    Password = request.Password,
                    UserKey = new Hashids(config.SecretKey).EncodeLong(DateTime.Now.Ticks)
                };

                executionEngine.Handle(cmd);

                return Ok(cmd.UserKey);
            }

            return BadRequest(ModelState);
        }
    }
}
