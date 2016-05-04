using System.Web.Http;
using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Engine.Commands.Locations;
using Trackwane.Management.Engine.Queries.Locations;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class LocationsController : BaseManagementController
    {
        private const string RESOURCE_URL = "locations/{key}";
        private const string COLLECTION_URL = "locations";

        public LocationsController(IExecutionEngine dispatcher) : base(dispatcher)
        {
        }

        [Secured, Managers, HttpPost, Route(RESOURCE_URL)]
        public void UpdateLocation(string organizationKey, string key, UpdateLocationModel model)
        {
            dispatcher.Handle(new UpdateLocation(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, key)
            {
                Coordinates = model.Coordinates,
                Name = model.Name
            });
        }

        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public LocationDetails FindByKey(string organizationKey, string key)
        {
            return dispatcher.Query<FindById>(AppKeyFromHeader, organizationKey).Execute(key);
        }

        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<LocationSummary> FindBySearchCriteria(string organizationKey, SearchLocationsModel model)
        {
            return dispatcher.Query<FindBySearchCriteria>(AppKeyFromHeader, organizationKey).Execute();
        }

        [Secured, Managers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveBoundary(string organizationKey, string key)
        {
            dispatcher.Handle(new ArchiveLocation(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, key));
        }

        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void RegisterLocation(string organizationKey, RegisterLocationModel model)
        {
            dispatcher.Handle(new RegisterLocation(AppKeyFromHeader, CurrentClaims.UserId, organizationKey,
                model.Name,
                model.Coordinates != null ? new Geo.IO.GeoJson.GeoJsonReader().Read(model.Coordinates) as Point : null,
                model.Key)
                );
        }
    }
}
