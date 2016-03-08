using System.Web.Http;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;

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
            executionEngine.Send(new CheckFramework(CurrentClaims.UserId));
        }
    }
}
