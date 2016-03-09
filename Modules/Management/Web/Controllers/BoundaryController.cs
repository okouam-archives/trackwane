using System.Web.Mvc;
using Geo.Geometries;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Engine.Commands.Boundaries;
using Trackwane.Management.Engine.Queries.Boundaries;

namespace Trackwane.Management.Web.Controllers
{
    [RoutePrefix("organizations/{oid}/boundaries")]
    public class BoundaryController : Controller
    {
        private readonly IExecutionEngine dispatcher;

        public BoundaryController(IExecutionEngine dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet, Route("{boundaryId}")]
        public ViewResult Get(string oid, string boundaryId)
        {
            var result = dispatcher.Query<FindById>(oid).Execute(boundaryId);

            return View("Show", result);
        }

        [HttpPost, Route("{boundaryId}")]
        public RedirectToRouteResult Update(string oid, string boundaryId, string name, string wkt)
        {
            var reader = new Geo.IO.Wkt.WktReader();
            var polygon = reader.Read(wkt);

            dispatcher.Send(new UpdateBoundary(null, boundaryId, oid)
            {
                Name = name,
                Coordinates = polygon as Polygon
            });

            return RedirectToAction("Index");
        }

        [HttpPost, Route("")]
        public RedirectToRouteResult Register(string oid, string name, string wkt)
        {
            var reader = new Geo.IO.Wkt.WktReader();
            var polygon = reader.Read(wkt);
            dispatcher.Send(new CreateBoundary(null, oid, name, polygon as Polygon, BoundaryType.ExclusionZone, null));
            return RedirectToAction("Index");
        }

        [HttpGet, Route("")]
        public ViewResult Index(string oid)
        {
            var result = dispatcher.Query<FindBySearchCriteria>(oid).Execute(oid);

            return View("Index", result);
        }
    }
}