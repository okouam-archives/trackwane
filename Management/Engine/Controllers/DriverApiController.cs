using System.Security.Claims;
using System.Web.Http;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;
using Trackwane.Management.Commands.Drivers;
using Trackwane.Management.Queries.Drivers;
using Trackwane.Management.Responses.Drivers;

namespace Trackwane.Management.Server.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class DriverApiController : BaseManagementController
    {
        private const string RESOURCE_URL = "drivers/{key}";
        private const string COLLECTION_URL = "drivers";

        public DriverApiController(IExecutionEngine dispatcher) : base(dispatcher)
        {
        }

        [Secured, Managers, HttpPost, Route(RESOURCE_URL)]
        public void UpdateDriver(string organizationKey, string key, UpdateDriverModel model)
        {
            var identity = User.Identity as ClaimsIdentity;

            var cmd = new UpdateDriver(identity.Name, organizationKey, key)
            {
                Name = model.Name
            };

            dispatcher.Send(cmd);
        }

        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public DriverDetails FindById(string organizationKey, string key) =>
            dispatcher.Query<FindById>(organizationKey).Execute(key);

        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<DriverSummary> FindBySearchCriteria(string organizationKey, string name) =>
            dispatcher.Query<FindBySearchCriteria>(organizationKey).Execute(name);

        [Secured, Managers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveDriver(string organizationKey, string key) =>
            dispatcher.Send(new ArchiveDriver(CurrentClaims.UserId, organizationKey, key));

        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void CreateDriver(string organizationKey, CreateDriverModel model) =>
            dispatcher.Send(new RegisterDriver(CurrentClaims.UserId, organizationKey, model.Name, model.Key));
    }
}
