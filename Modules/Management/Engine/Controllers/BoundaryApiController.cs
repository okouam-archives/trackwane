using System;
using System.Web.Http;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;
using Trackwane.Management.Engine.Commands.Boundaries;
using Trackwane.Management.Engine.Queries.Boundaries;
using Trackwane.Management.Models.Boundaries;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class BoundaryApiController : BaseManagementController
    {
        private const string RESOURCE_URL = "boundaries/{key}";
        private const string COLLECTION_URL = "boundaries";

        public BoundaryApiController(IExecutionEngine dispatcher) : base(dispatcher)
        {
        }

        [Secured, Managers, HttpPost, Route(RESOURCE_URL)]
        public void UpdateBoundary(string organizationKey, string key, UpdateBoundaryModel model) =>
            dispatcher.Send(new UpdateBoundary(CurrentClaims.UserId, organizationKey, key) {Coordinates = model.Coordinates, Name = model.Name});

        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public BoundaryDetails FindById(string organizationKey, string id) =>
            dispatcher.Query<FindById>(organizationKey).Execute(id);
        
        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<BoundarySummary> FindBySearchCriteria(string organizationKey, string name) =>
            dispatcher.Query<FindBySearchCriteria>(organizationKey).Execute(name);
        
        [Secured, Managers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveBoundary(string organizationKey, string key) =>
            dispatcher.Send(new ArchiveBoundary(CurrentClaims.UserId, organizationKey, key));
        
        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void CreateBoundary(string organizationKey, CreateBoundaryModel model) =>
            dispatcher.Send(new CreateBoundary(CurrentClaims.UserId, organizationKey, model.Name, model.Coordinates,
                (BoundaryType) Enum.Parse(typeof(BoundaryType), model.Type), model.Key));
        
    }
}
