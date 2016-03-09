using System;
using Geo.Geometries;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Data.Engine.Commands
{
    public class SaveSensorReading : SystemCommand
    {
        public string HardwareId { get; set; }

        public DateTime Timestamp { get; set; }

        public int? Speed { get; set; }

        public int? Orientation { get; set; }

        public int? Petrol { get; set; }

        public double? BatteryLevel { get; set; }

        public decimal? Distance { get; set; }

        public Point Coordinates { get; set; }

        public double? Heading { get; set; }

        public string RawDataId { get; set; }
    }
}
