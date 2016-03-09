using System.Web.Http;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;
using Trackwane.Management.Engine.Commands.Trackers;
using Trackwane.Management.Engine.Queries.Trackers;
using Trackwane.Management.Models.Trackers;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class TrackerApiController : BaseManagementController
    {
        private const string RESOURCE_URL = "trackers/{key}";
        private const string COLLECTION_URL = "trackers";

        public TrackerApiController(IExecutionEngine dispatcher) : base(dispatcher)
        {
        }

        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public TrackerDetails FindByKey(string organizationKey, string key) =>
            dispatcher.Query<FindById>(organizationKey).Execute(key);

        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<TrackerSummary> FindBySearchCriteria(string organizationKey, string name) =>
            dispatcher.Query<FindBySearchCriteria>(organizationKey).Execute(name);
        
        [Secured, Managers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveTracker(string organizationKey, string key) =>
            dispatcher.Send(new ArchiveTracker(CurrentClaims.UserId, organizationKey, key));
        
        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void RegisterTracker(string organizationKey, RegisterTrackerModel model) =>
            dispatcher.Send(new RegisterTracker(CurrentClaims.UserId, organizationKey, model.HardwareId, model.Model, model.Key));
    }
}
