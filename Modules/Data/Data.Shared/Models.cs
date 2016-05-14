using System;

// ReSharper disable CheckNamespace
namespace Trackwane.Data.Shared.Models
// ReSharper restore CheckNamespace
{
    public class SaveSensorReadingModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Heading { get; set; }
        public int? Orientation { get; set; }
        public int? Speed { get; set; }
        public decimal? Distance { get; set; }
        public double? BatteryLevel { get; set; }
        public int? Petrol { get; set; }
        public DateTime Timestamp { get; set; }
        public string HardwareId { get; set; }
    }
}
