using System.Web.Http;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Tests.Fakes
{
    public class CheckFrameworkController : BaseApiController
    {
        private readonly IExecutionEngine executionEngine;

        public CheckFrameworkController(IExecutionEngine executionEngine)
        {
            this.executionEngine = executionEngine;
        }

        [Route("check"), HttpPost]
        public void RunCheck()
        {
            executionEngine.Handle(new CheckFramework(CurrentClaims.UserId));
        }
    }
}
