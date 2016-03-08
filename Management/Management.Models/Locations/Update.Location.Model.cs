using Geo.Geometries;

namespace Trackwane.Management.Responses.Locations
{
    public class UpdateLocationModel
    {
        public string Name { get; set; }

        public Point Coordinates { get; set; }
    }
}
