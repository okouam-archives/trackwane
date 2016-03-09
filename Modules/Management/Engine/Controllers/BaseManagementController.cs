using System.Web.Http;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class BaseManagementController : BaseApiController
    {
        protected readonly IExecutionEngine dispatcher;

        protected BaseManagementController(IExecutionEngine dispatcher)
        {
            this.dispatcher = dispatcher;
        }
    }
}
