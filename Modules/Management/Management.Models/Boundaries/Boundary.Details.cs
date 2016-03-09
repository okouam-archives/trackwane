using Geo.Geometries;

namespace Trackwane.Management.Models.Boundaries
{
    public class BoundaryDetails
    {
        public Polygon Coordinates { get; set; }

        public bool IsArchived { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
