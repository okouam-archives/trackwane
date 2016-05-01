using System;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Contracts.Models;
using Trackwane.AccessControl.Engine.Commands.Application;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class ApplicationController : BaseApiController
    {
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig config;

        public ApplicationController(IExecutionEngine executionEngine, IPlatformConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [HttpPost, Route("application")]
        public string RegisterApplication(RegisterApplicationModel model)
        {
            var secretKey = config.Get("secret-key");

            if (secretKey != model.SecretKey)
            {
                throw new Exception("The platform secret key provided is invalid");
            }

            var cmd = new RegisterApplication(AppKeyFromHeader)
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Password = model.Password,
                UserKey = new Hashids(secretKey).EncodeLong(DateTime.Now.Ticks)
            };

            executionEngine.Send(cmd);

            return cmd.UserKey;
        }
    }
}
