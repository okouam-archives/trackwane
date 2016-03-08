using System;
using System.Globalization;
using Geo.Geometries;
using Trackwane.Data.Domain;
using Trackwane.Data.Engine.Commands;
using Trackwane.Data.Models;

namespace Trackwane.Data.Tests.Helpers
{
    internal partial class Scenario
    {
        public class _RawData
        {
            public static void With(string id, string hardwareId, Point coordinates = null, int? speed = null,
                decimal? distance = null, int? petrol = null, int? batteryLevel = null, int? orientation = null,
                DateTime? timestamp = null)
            {
                Send(new SaveSensorReading(hardwareId, DateTime.Now)
                {
                    RawDataId = id,
                    Coordinates = coordinates,
                    Speed = speed,
                    Petrol = petrol,
                    Distance = distance,
                    BatteryLevel = batteryLevel,
                    Orientation = orientation
                });
            }

            public static void With(Point coordinates = null, int? speed = null, decimal? distance = null,
                int? petrol = null, int? batteryLevel = null, int? orientation = null, DateTime? timestamp = null)
            {
                Client.SaveSensorReading(new SaveSensorReadingModel("HARDWARE-ID: " + DateTime.Now, DateTime.Now)
                {
                    RawDataId = DateTime.Now.ToString(),
                    Coordinates = coordinates,
                    Speed = speed,
                    Petrol = petrol,
                    Distance = distance,
                    BatteryLevel = batteryLevel,
                    Orientation = orientation,
                    Timestamp = timestamp ?? DateTime.Now
                });
            }

            public static void With(DateTime from)
            {
                With(DateTime.Now.ToString(CultureInfo.InvariantCulture), "HARDWARE-ID: " + DateTime.Now, timestamp: from);
            }
        }
    }
}
