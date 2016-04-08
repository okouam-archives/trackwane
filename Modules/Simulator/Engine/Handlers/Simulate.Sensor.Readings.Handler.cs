using System;
using System.Linq;
using paramore.brighter.commandprocessor;
using Serilog;
using Trackwane.Data.Shared.Client;
using Trackwane.Data.Shared.Models;
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

            if (events.Count > 0)
            {
                Log.Information("{PositionNum} bus positions were retrieved", events.Count);
            }
            else
            {
                Log.Error("No bus positions could be retrieved");
            }
            
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
                        Distance = evt.Distance,
                        Heading = evt.Heading,
                        HardwareId = evt.HardwareId,
                        Petrol = evt.Petrol,
                        Timestamp = evt.Timestamp
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unable to save sensor reading for {ID}", evt.HardwareId);
                }
            }

            return base.Handle(command);
        }

        private static string GetBuseIds(SimulateSensorReadings command)
        {
            return string.Join(", ", command.Buses.Select(x => x.ToString()));
        }
    }
}
