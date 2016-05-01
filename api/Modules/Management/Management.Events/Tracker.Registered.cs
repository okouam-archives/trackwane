using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class TrackerRegistered : DomainEvent
    {
        public string TrackerKey { get; set; }

        public string OrganizationKey { get; set; }

        public string Model { get; set; }

        public string HardwareId { get; set; }
    }
}
