using Geo.Geometries;
using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class BoundaryCreated : DomainEvent
    {
        public string BoundaryKey { get; set; }

        public string Name { get; set; }

        public string OrganizationKey { get; set; }

        public Polygon Coordinates { get; set; }
    }
}
