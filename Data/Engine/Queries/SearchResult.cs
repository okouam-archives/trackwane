using System;
using Geo.Geometries;

namespace Trackwane.Data.Engine.Queries
{
    public class SearchResult
    {
        public Point Coordinates { get; set; }

        public int? Heading { get; set; }

        public int? Orientation { get; set; }

        public int? BatteryLevel { get; set; }

        public DateTime Timestamp { get; internal set; }

        public decimal? Distance { get; set; }

        public string HardwareId { get; internal set; }

        public int? Petrol { get; set; }

        public int? Speed { get; set; }
    }
}
