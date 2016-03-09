using Geo.Geometries;

namespace Trackwane.Management.Models.Locations
{
    public class LocationDetails
    {
        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public Point Coordinates { get; set; }
    }
}
