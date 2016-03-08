using Geo.Geometries;
using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class LocationUpdated : DomainEvent
    {
        public State Current { get; set; }

        public State Previous { get; set; }

        public string OrganizationKey { get; set; }

        public string LocationKey { get; set; }

        public string Name { get; set; }

        public class State
        {
            public string Name { get; set; }

            public Point Coordinates { get; set; }
        }
    }
}
