using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class TrackerArchived : DomainEvent
    {
        public string TrackerKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
