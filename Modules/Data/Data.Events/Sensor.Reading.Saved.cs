using System;
using Geo.Geometries;
using Trackwane.Framework.Common;

namespace Trackwane.Data.Events
{
    public class SensorReadingSaved : DomainEvent
    {
        public string ReadingKey { get; set; }

        public string TrackerKey { get; set; }

        public string OrganizationKey { get; set; }

        public string HardwareId { get; set; }

        public double? Speed { get; set; }

        public double? Orientation { get; set; }

        public double? Petrol { get; set; }

        public double? BatteryLevel { get; set; }

        public decimal? Distance { get; set; }

        public DateTime Timestamp { get; set; }

        public Point Coordinates { get; set; }

        public double? Heading { get; set; }
    }
}
