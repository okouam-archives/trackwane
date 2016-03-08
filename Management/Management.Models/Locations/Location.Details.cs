using Geo.Geometries;

namespace Trackwane.Management.Responses.Locations
{
    public class LocationDetails
    {
        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public Point Coordinates { get; set; }
    }
}
