using System;
using System.Web.Http;
using HashidsNet;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Engine.Commands.Trackers;
using Trackwane.Management.Engine.Queries.Trackers;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class SensorsController : BaseManagementController
    {
        private readonly IPlatformConfig config;
        private const string RESOURCE_URL = "sensors/{key}";
        private const string COLLECTION_URL = "sensors";

        public SensorsController(IExecutionEngine dispatcher, IPlatformConfig config) : base(dispatcher)
        {
            this.config = config;
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
        public IHttpActionResult RegisterTracker(string organizationKey, RegisterTrackerModel model)
        {
            var key = new Hashids(config.SecretKey).EncodeLong(DateTime.Now.Ticks);

            dispatcher.Handle(new RegisterSensor(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, model.HardwareId, model.Model,
                model.Identifier, key));

            return Created("sdsdf", key);
        }
    }
}
