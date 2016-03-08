using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using MoreLinq;
using Newtonsoft.Json;
using Trackwane.Simulator.Domain;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Engine.Queries
{
    public class FindVehiclePositions
    {
        private readonly IProvidePositions positionProvider;
        private readonly FindVehicles findVehiclesQuery;

        public FindVehiclePositions(IProvidePositions positionProvider, FindVehicles findVehiclesQuery)
        {
            this.positionProvider = positionProvider;
            this.findVehiclesQuery = findVehiclesQuery;
        }

        public IEnumerable<GpsEvent> Execute(params int[] vehicles)
        {
            if (vehicles.Length == 0)
            {
                vehicles = findVehiclesQuery.Execute().ToArray();
            }

            log.Debug($"Getting positions for {vehicles.Length} vehicles");

            var batches = vehicles.Batch(10);

            var positions = from batch in batches
                            from position in positionProvider.Retrieve("vid", batch.Select(x => x.ToString()).ToArray())
                            orderby position.Id
                            select position;

            var result = GenerateMetadata(positions).ToList();

            foreach (var position in result)
            {
                log.Debug(JsonConvert.SerializeObject(position, Formatting.Indented));
            }

            return result;
        }

        /* Private */

        public IEnumerable<GpsEvent> GenerateMetadata(IEnumerable<Position> positions)
        {
            var rand = new Random();

            return positions.Select(x => new GpsEvent
            {
                HardwareId = "CTA-BUS-" + x.Id,
                Speed = rand.Next(0, 130),
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Orientation = x.Heading,
                Petrol = rand.Next(0, 100),
                Timestamp = x.Timestamp
            });
        }

        private readonly ILog log = LogManager.GetLogger(typeof(FindVehiclePositions));
    }
}
