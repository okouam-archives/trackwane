using Geo.Geometries;
using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class BoundaryUpdated : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string BoundaryKey { get; set; }

        public State Current { get; set; }

        public State Previous { get; set; }

        public class State
        {
            public string Name { get; set; }

            public Polygon Coordinates { get; set; }
        }
    }
}
