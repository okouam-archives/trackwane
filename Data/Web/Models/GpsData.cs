namespace Trackwane.Data.Web.Models
{
    public class GpsData
    {
        public string HardwareId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int? Orientation { get; set; }

        public int? Heading { get; set; }

        public decimal? Distance { get; set; }

        public int? BatteryLevel { get; set; }

        public int? Petrol { get; set; }

        public int? Speed { get; set; }
    }
}