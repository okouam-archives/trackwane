using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Serilog;
using Trackwane.Simulator.Domain;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Engine.Queries
{
    public class GetVehicleReadings
    {
        private readonly IProvideReadings readingProvider;
        private readonly FindVehicles findVehiclesQuery;

        public GetVehicleReadings(IProvideReadings readingProvider, FindVehicles findVehiclesQuery)
        {
            this.readingProvider = readingProvider;
            this.findVehiclesQuery = findVehiclesQuery;
        }

        public IEnumerable<SensorReading> Execute(params int[] vehicles)
        {
            if (vehicles.Length == 0)
            {
                vehicles = findVehiclesQuery.Execute().ToArray();
            }

            var batches = vehicles.Batch(10);

            var readings = from batch in batches
                            from position in readingProvider.Retrieve("vid", batch.Select(x => x.ToString()).ToArray())
                            orderby position.Id
                            select position;

            var result = GenerateMetadata(readings).ToList();

            foreach (var position in result)
            {
                Log.Information("A {@SensorReading} was retrieved", position);
            }

            return result;
        }

        /* Private */

        public IEnumerable<SensorReading> GenerateMetadata(IEnumerable<Position> positions)
        {
            var rand = new Random();

            return positions.Select(x => new SensorReading
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
    }
}
