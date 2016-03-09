using System.Web.Mvc;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Engine.Commands.Drivers;
using Trackwane.Management.Engine.Queries.Drivers;

namespace Trackwane.Management.Web.Controllers
{
    [RoutePrefix("organizations/{oid}")]
    public class DriverController : Controller
    {
        private readonly IExecutionEngine dispatcher;

        public DriverController(IExecutionEngine dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet, Route("{driverId}")]
        public ViewResult Get(string oid, string driverId)
        {
            var result = dispatcher.Query<FindById>(oid).Execute(driverId);

            return View("Index", result);
        }

        [HttpPost, Route("{driverId}")]
        public RedirectToRouteResult Update(string oid, string driverId, string name)
        {
            dispatcher.Send(new UpdateDriver(null, oid, driverId)
            {
                Name = name
            });

            return RedirectToAction("Index");
        }

        [HttpPost, Route("")]
        public RedirectToRouteResult Register(string oid, string name)
        {
            dispatcher.Send(new RegisterDriver(null, oid, name, null));

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