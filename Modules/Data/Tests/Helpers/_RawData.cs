using System;
using System.Globalization;
using Geo.Geometries;
using Trackwane.Data.Contracts.Models;

namespace Trackwane.Data.Tests.Helpers
{
    internal partial class Scenario
    {
        public class _SaveSensorReading
        {
            public static void With(string id, string hardwareId, Point coordinates = null, int? speed = null,
                decimal? distance = null, int? petrol = null, int? batteryLevel = null, int? orientation = null,
                DateTime? timestamp = null)
            {
                Client.SaveSensorReading(new SaveSensorReadingModel
                {
                    Latitude = coordinates.Coordinate.Latitude,
                    Speed = speed,
                    Petrol = petrol,
                    HardwareId = hardwareId ?? "HARDWARE-ID: " + DateTime.Now,
                    Distance = distance,
                    BatteryLevel = batteryLevel,
                    Orientation = orientation,
                    Timestamp = timestamp ?? DateTime.Now
                });
            }

            public static void With(Point coordinates = null, int? speed = null, decimal? distance = null,
                int? petrol = null, int? batteryLevel = null, int? orientation = null, DateTime? timestamp = null)
            {
                With(Guid.NewGuid().ToString(), null, coordinates, speed, distance, petrol, batteryLevel, orientation, timestamp);
            }

            public static void With(DateTime from)
            {
                With(DateTime.Now.ToString(CultureInfo.InvariantCulture), "HARDWARE-ID: " + DateTime.Now, timestamp: from);
            }
        }
    }
}
