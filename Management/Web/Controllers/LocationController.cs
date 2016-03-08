using System.Web.Mvc;
using Geo.Geometries;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Locations;
using Trackwane.Management.Queries.Locations;

namespace Trackwane.Management.Web.Controllers
{
    [RoutePrefix("organizations/{oid}/locations")]
    public class LocationController : Controller
    {
        private readonly IExecutionEngine dispatcher;

        public LocationController(IExecutionEngine dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet, Route("{locationId}")]
        public ViewResult Get(string oid, string locationId)
        {
            var result = dispatcher.Query<FindById>(oid).Execute(locationId);

            return View("Show", result);
        }

        [HttpPost, Route("{locationId}")]
        public RedirectToRouteResult Update(string oid, string locationId, string name, double? longitude, double? latitude)
        {
            var newName = string.IsNullOrEmpty(name) ? null : name;

            var newCoordinates = longitude.HasValue && latitude.HasValue ? new Point(latitude.Value, longitude.Value) : null;

            dispatcher.Send(new UpdateLocation(null, locationId, oid)
            {
                Coordinates = newCoordinates,
                Name = newName
            });

            return RedirectToAction("Index");
        }

        [HttpPost, Route("locations")]
        public RedirectToRouteResult Register(string oid, string name, double longitude, double latitude)
        {
            dispatcher.Send(new RegisterLocation(null, oid, name, new Point(latitude, longitude), null));

            return RedirectToAction("Index");
        }

        [HttpGet, Route("locations")]
        public ViewResult Index(string oid)
        {
            var result = dispatcher.Query<FindBySearchCriteria>(oid).Execute(oid);

            return View("Index", result);
        }
    }
}