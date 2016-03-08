using System;
using System.Web.Http;
using Geo.Geometries;
using StructureMap;
using Trackwane.Data.Engine.Queries;
using Trackwane.Data.Web.Models;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.Data.Web.Controllers
{
    public class DataController : ApiController
    {
        [HttpGet, Route("data")]
        public ResponsePage<SearchResult> Search(string hardwareId, DateTime from, DateTime to)
        {
            var container = new Container();

            var queryHandler = container.GetInstance<IHandleQueries>();

            return queryHandler.Execute(new FindBySearchCriteria(new FindBySearchCriteria.Params
            {
               HardwareId = hardwareId,
               From = from,
               To = to
            }));
        }

        [HttpPost, Route("data")]
        public void Save(GpsData model)
        {
            var container = new Container();

            var dispatcher = container.GetInstance<paramore.brighter.commandprocessor.IAmACommandProcessor>();

            dispatcher.Send(new SaveRawData(model.HardwareId, DateTime.Now)
            {
                Speed = model.Speed,
                Petrol = model.Petrol,
                BatteryLevel = model.BatteryLevel,
                Distance = model.Distance,
                Heading = model.Heading,
                Orientation = model.Orientation,
                Coordinates = new Point(model.Latitude, model.Longitude)
            });
        }
    }
}
