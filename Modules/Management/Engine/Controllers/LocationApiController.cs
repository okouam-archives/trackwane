using System.Web.Http;
using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Engine.Commands.Locations;
using Trackwane.Management.Engine.Queries.Locations;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class LocationApiController : BaseManagementController
    {
        private const string RESOURCE_URL = "locations/{key}";
        private const string COLLECTION_URL = "locations";

        public LocationApiController(IExecutionEngine dispatcher) : base(dispatcher)
        {
        }

        [Secured, Managers, HttpPost, Route(RESOURCE_URL)]
        public void UpdateLocation(string organizationKey, string key, UpdateLocationModel model) =>
            dispatcher.Send(new UpdateLocation(CurrentClaims.UserId, organizationKey, key) {Coordinates = model.Coordinates, Name = model.Name});
        
        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public LocationDetails FindByKey(string organizationKey, string key) =>
            dispatcher.Query<FindById>(organizationKey).Execute(key);
        
        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<LocationSummary> FindBySearchCriteria(string organizationKey, SearchLocationsModel model) =>
            dispatcher.Query<FindBySearchCriteria>(organizationKey).Execute();
        
        [Secured, Managers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveBoundary(string organizationKey, string key) =>
            dispatcher.Send(new ArchiveLocation(CurrentClaims.UserId, organizationKey, key));
        
        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void RegisterLocation(string organizationKey, RegisterLocationModel model) =>
            dispatcher.Send(new RegisterLocation(CurrentClaims.UserId, organizationKey, 
                model.Name, 
                model.Coordinates != null ? new Geo.IO.GeoJson.GeoJsonReader().Read(model.Coordinates) as Point : null, 
                model.Key)
            );
    }
}
