using Geo.Geometries;
using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class LocationRegistered : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string LocationKey { get; set; }

        public string Name { get; set; }

        public Point Coordinates { get; set; }
    }
}
