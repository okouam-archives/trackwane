using System;

namespace Trackwane.Simulator.Domain
{
    public class Position
    {
        public int Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int Heading { get; set; }

        public DateTime Timestamp { get; set; }
        
        public Position(int id, decimal latitude, decimal longitude, int heading, DateTime timestamp)
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
