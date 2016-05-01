using System;

namespace Trackwane.Simulator.Domain
{
    public class SensorReading
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
        
        public int Orientation { get; set; }

        public int Speed { get; set; }

        public int Petrol { get; set; }

        public DateTime Timestamp { get; set; }

        public string HardwareId { get; set; }

        public int Heading { get; set; }

        public int Distance { get; set; }

        public override string ToString()
        {
            return HardwareId;
        }
    }
}