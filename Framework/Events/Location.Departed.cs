using Trackwane.Framework.Common;

namespace Trackwane.Framework.Events
{
    public class LocationDeparted : DomainEvent
    {
        public string LocationKey { get; set; }

        public string OrganizationKey { get; set; }

        public string TrackerKey { get; set; }
    }
}
