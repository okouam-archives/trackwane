using System;

namespace Trackwane.Simulator.Domain
{
    public class Position
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Heading { get; set; }

        public DateTime Timestamp { get; set; }
        
        public Position(int id, double latitude, double longitude, int heading, DateTime timestamp)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
            Heading = heading;
            Timestamp = timestamp;
        }
        
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
