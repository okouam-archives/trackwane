using System;
using System.Web.Http;
using Geo.Geometries;
using Trackwane.Data.Contracts.Models;
using Trackwane.Data.Engine.Commands;
using Trackwane.Data.Engine.Queries;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Engine.Controller
{
    public class DataController : ApiController
    {
        private readonly IExecutionEngine executionEngine;

        public DataController(IExecutionEngine executionEngine)
        {
            this.executionEngine = executionEngine;
        }

        [HttpGet, Route("data")]
        public ResponsePage<SearchResult> Search(string hardwareId = null, DateTime? from = null, DateTime? to = null)
        {           
            return executionEngine.Query<FindBySearchCriteria>().Execute(hardwareId, from, to);
        }

        [HttpPost, Route("data")]
        public void Save(SaveSensorReadingModel model)
        {
            executionEngine.Send(new SaveSensorReading
            {
                HardwareId = model.HardwareId,
                Timestamp = DateTime.Now,
                Speed = model.Speed,
                Petrol = model.Petrol,
                BatteryLevel = model.BatteryLevel,
                Distance = model.Distance,
                Heading = model.Heading,
                Orientation = model.Orientation,
                Coordinates = new Point(model.Latitude.Value, model.Longitude.Value)
            });
        }
    }
}
