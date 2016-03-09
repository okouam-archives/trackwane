using System.Web.Mvc;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Engine.Commands.Vehicles;
using Trackwane.Management.Engine.Queries.Vehicles;

namespace Trackwane.Management.Web.Controllers
{
    [RoutePrefix("organizations/{organizationKey}/vehicles")]
    public class VehicleController : Controller
    {
        private readonly IExecutionEngine dispatcher;

        public VehicleController(IExecutionEngine dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet, Route("{vehicleId}")]
        public ViewResult Get(string organizationKey, string vehicleId)
        {
            var result = dispatcher.Query<FindById>(organizationKey).Execute(vehicleId);

            return View("Index", result);
        }

        [HttpPost, Route("{vehicleId}")]
        public RedirectToRouteResult Update(string oid, string vehicleId, string name)
        {
            dispatcher.Send(new UpdateVehicle(null, vehicleId, name, oid));

            return RedirectToAction("Index");
        }

        [HttpPost, Route("")]
        public RedirectToRouteResult Register(string oid, string name)
        {
            dispatcher.Send(new RegisterVehicle(null, oid, name));

            return RedirectToAction("Index");
        }

        [HttpGet, Route("")]
        public ViewResult Index(string oid)
        {
            var result = dispatcher.Query<FindBySearchCriteria>(oid).Execute(new FindBySearchCriteria.Criteria());

            return View("Index", result);
        }
    }
}