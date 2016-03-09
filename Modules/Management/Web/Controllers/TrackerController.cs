using System.Web.Mvc;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Engine.Commands.Trackers;
using Trackwane.Management.Engine.Queries.Trackers;

namespace Trackwane.Management.Web.Controllers
{
    [RoutePrefix("organizations/{oid}/trackers")]
    public class TrackerController : Controller
    {
        private readonly IExecutionEngine dispatcher;

        public TrackerController(IExecutionEngine dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet, Route("{trackerId}")]
        public ViewResult Get(string oid, string trackerId)
        {
            var result = dispatcher.Query<FindById>(oid).Execute(trackerId);

            return View("Index", result);
        }

        [HttpPost, Route("")]
        public RedirectToRouteResult Register(string oid, string hardwareId, string model)
        {
            dispatcher.Send(new RegisterTracker(null, oid, hardwareId, model));

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