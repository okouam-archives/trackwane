using System.Web.Http;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Engine.Commands.Trackers;
using Trackwane.Management.Engine.Queries.Trackers;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class TrackersController : BaseManagementController
    {
        private const string RESOURCE_URL = "trackers/{key}";
        private const string COLLECTION_URL = "trackers";

        public TrackersController(IExecutionEngine dispatcher) : base(dispatcher)
        {
        }

        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public TrackerDetails FindByKey(string organizationKey, string key)
        {
            return dispatcher.Query<FindById>(AppKeyFromHeader, organizationKey).Execute(key);
        }

        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<TrackerSummary> FindBySearchCriteria(string organizationKey, string name)
        {
            return dispatcher.Query<FindBySearchCriteria>(AppKeyFromHeader, organizationKey).Execute(name);
        }

        [Secured, Managers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveTracker(string organizationKey, string key)
        {
            dispatcher.Handle(new ArchiveTracker(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, key));
        }

        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void RegisterTracker(string organizationKey, RegisterTrackerModel model)
        {
            dispatcher.Handle(new RegisterTracker(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, model.HardwareId, model.Model,
                model.Key));
        }
    }
}
