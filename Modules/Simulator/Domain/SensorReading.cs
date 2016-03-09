using System;

namespace Trackwane.Simulator.Domain
{
    public class SensorReading
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
        
        public int Orientation { get; set; }

        public int Speed { get; set; }

        public int Petrol { get; set; }

        public DateTime Timestamp { get; set; }

        public string HardwareId { get; set; }

        public override string ToString()
        {
            return HardwareId;
        }
    }
}