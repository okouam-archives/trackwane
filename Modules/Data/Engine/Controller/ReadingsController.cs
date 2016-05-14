using System;
using System.Web.Http;
using Geo.Geometries;
using HashidsNet;
using Trackwane.Data.Engine.Commands;
using Trackwane.Data.Engine.Queries;
using Trackwane.Data.Shared.Models;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Engine.Controller
{
    public class ReadingsController : ApiController
    {
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig config;

        public ReadingsController(IExecutionEngine executionEngine, IPlatformConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [HttpGet, Route("data")]
        public ResponsePage<SearchResult> Search(string hardwareId = null, DateTime? from = null, DateTime? to = null)
        {           
            return executionEngine.Query<FindBySearchCriteria>(null).Execute(hardwareId, from, to);
        }

        [HttpPost, Route("data/{applicationKey}/{organizationKey}")]
        public void Save(SaveSensorReadingModel model)
        {
            executionEngine.Handle(new SaveSensorReading
            {
                RawDataId = Guid.NewGuid().ToString(),
                HardwareId = model.HardwareId,
                Timestamp = model.Timestamp,
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
