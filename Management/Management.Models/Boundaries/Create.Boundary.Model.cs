using Geo.Geometries;

namespace Trackwane.Management.Responses.Boundaries
{
    public class CreateBoundaryModel
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public Polygon Coordinates { get; set; }

        public string Type { get; set; }
    }
}
