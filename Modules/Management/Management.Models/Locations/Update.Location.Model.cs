using Geo.Geometries;

namespace Trackwane.Management.Models.Locations
{
    public class UpdateLocationModel
    {
        public string Name { get; set; }

        public Point Coordinates { get; set; }
    }
}
