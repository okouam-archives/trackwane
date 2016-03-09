using System.Web.Mvc;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Engine.Commands.Alerts;
using Trackwane.Management.Engine.Queries.Alerts;

namespace Trackwane.Management.Web.Controllers
{
    [RoutePrefix("organizations/{oid}/alerts")]

    public class AlertController : Controller
    {
        private readonly IExecutionEngine dispatcher;

        public AlertController(IExecutionEngine dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet, Route("{id}")]
        public ViewResult Get(string oid, string id)
        {
            var result = dispatcher.Query<FindByKey>(oid).Execute(id);

            return View("Show", result);
        }

        [HttpPost, Route("{id}")]
        public RedirectToRouteResult Update(string oid, string id, string name)
        {
            dispatcher.Send(new UpdateAlert(null, id, name, oid));

            return RedirectToAction("Index");
        }

        [HttpPost, Route("")]
        public RedirectToRouteResult Register(string oid, string name)
        {
            dispatcher.Send(new CreateAlert(null, null, oid, name));

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