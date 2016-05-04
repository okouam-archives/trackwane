using System;
using System.Web.Http;
using Geo.Geometries;
using Trackwane.Data.Engine.Commands;
using Trackwane.Data.Engine.Queries;
using Trackwane.Data.Shared.Models;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Engine.Controller
{
    public class ReadingsController : ApiController
    {
        private readonly IExecutionEngine executionEngine;

        public ReadingsController(IExecutionEngine executionEngine)
        {
            this.executionEngine = executionEngine;
        }

        [HttpGet, Route("data")]
        public ResponsePage<SearchResult> Search(string hardwareId = null, DateTime? from = null, DateTime? to = null)
        {           
            return executionEngine.Query<FindBySearchCriteria>(null).Execute(hardwareId, from, to);
        }

        [HttpPost, Route("data")]
        public void Save(SaveSensorReadingModel model)
        {
            executionEngine.Handle(new SaveSensorReading
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
