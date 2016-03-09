using Geo.Geometries;

namespace Trackwane.Management.Models.Boundaries
{
    public class UpdateBoundaryModel
    {
        public string Name { get; set; }

        public Polygon Coordinates { get; set; }
    }
}
