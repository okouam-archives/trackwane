using System;
using Geo.Geometries;

namespace Trackwane.Data.Engine.Queries
{
    public class SearchResult
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
    }
}
