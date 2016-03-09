using System;
using System.Linq;
using paramore.brighter.commandprocessor;
using Serilog;
using Trackwane.Data.Client;
using Trackwane.Data.Models;
using Trackwane.Simulator.Engine.Commands;
using Trackwane.Simulator.Engine.Queries;

namespace Trackwane.Simulator.Engine.Handlers
{
    public class SimulateSensorReadingsHandler : RequestHandler<SimulateSensorReadings>
    {
        private readonly DataContext api;
        private readonly GetVehicleReadings query;

        public SimulateSensorReadingsHandler(DataContext api, GetVehicleReadings query) : base(null)
        {
            this.api = api;
            this.query = query;
        }

        public override SimulateSensorReadings Handle(SimulateSensorReadings command)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Log.Information("Retrieving positions from the CTA API for buses {Buses}", GetBuseIds(command));

            var events = query.Execute(command.Buses).ToList();

            Log.Information("{PositionNum} bus positions were retrieved", events.Count);

            var successes = 0;

            foreach (var evt in events)
            {
                try
                {
                    api.SaveSensorReading(new SaveSensorReadingModel
                    {
                        Latitude = evt.Latitude,
                        Longitude = evt.Longitude,
                        Orientation = evt.Orientation,
                        Speed = evt.Speed,
                        HardwareId = evt.HardwareId,
                        Petrol = evt.Petrol,
                        Timestamp = evt.Timestamp
                    });

                    successes++;
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unable to save sensor reading for {ID}", evt.HardwareId);
                }
            }

            if (successes < 1)
            {
                Log.Error("Unable to save any save readings for buses {Buses} - check the Data Module is up and running", GetBuseIds(command));
                throw new Exception("Unable to save any sensor readings - check the Data Module is up and running");
            }

            return base.Handle(command);
        }

        private static string GetBuseIds(SimulateSensorReadings command)
        {
            return string.Join(", ", command.Buses.Select(x => x.ToString()));
        }
    }
}
