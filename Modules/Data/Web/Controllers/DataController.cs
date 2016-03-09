using System;
using System.Web.Http;
using Geo.Geometries;
using StructureMap;
using Trackwane.Data.Engine.Commands;
using Trackwane.Data.Engine.Queries;
using Trackwane.Data.Models;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Web.Controllers
{
    public class DataController : ApiController
    {
        [HttpGet, Route("data")]
        public ResponsePage<SearchResult> Search(string hardwareId, DateTime from, DateTime to)
        {
            var container = new Container();

            var queryHandler = container.GetInstance<IExecutionEngine>();

            return queryHandler.Query<FindBySearchCriteria>().Execute(hardwareId, from, to);
        }

        [HttpPost, Route("data")]
        public void Save(SaveSensorReadingModel model)
        {
            var container = new Container();

            var dispatcher = container.GetInstance<paramore.brighter.commandprocessor.IAmACommandProcessor>();

            dispatcher.Send(new SaveSensorReading
            {
                HardwareId = model.HardwareId,
                Timestamp = DateTime.Now,
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
