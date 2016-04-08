using System;
using Geo.Geometries;
using Trackwane.Framework.Common;

// ReSharper disable CheckNamespace
namespace Trackwane.Data.Shared.Events
// ReSharper restore CheckNamespace
{
    public class SensorReadingSaved : DomainEvent
    {
        public string ReadingKey { get; set; }
        public string TrackerKey { get; set; }
        public string OrganizationKey { get; set; }
        public string HardwareId { get; set; }
        public Point Coordinates { get; set; }
        public DateTime? Timestamp { get; set; }
        public double? Speed { get; set; }
        public double? Orientation { get; set; }
        public double? Petrol { get; set; }
        public double? BatteryLevel { get; set; }
        public double? Heading { get; set; }
        public decimal? Distance { get; set; }
    }
}









