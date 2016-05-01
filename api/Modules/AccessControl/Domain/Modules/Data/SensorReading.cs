using System;
using Geo.Geometries;
using Trackwane.Framework.Common;

namespace Trackwane.Data.Domain
{
    public class SensorReading : AggregateRoot
    {
        public Point Coordinates { get; set; }

        public double? Heading { get; set; }

        public double? Orientation { get; set; }

        public double? BatteryLevel { get; set; }

        public DateTime Timestamp { get; internal set; }

        public decimal? Distance { get; set; }

        public string HardwareId { get; internal set; }

        public double? Petrol { get; set; }

        public double? Speed { get; set; }

        public SensorReading(string id, string hardwareId, DateTime timestamp)
        {
            Key = id;
            HardwareId = hardwareId;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return Key;
        }

        protected override void Apply(DomainEvent evt)
        {
            throw new NotImplementedException();
        }
    }
}
