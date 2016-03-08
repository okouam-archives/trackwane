using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class LocationArchived : DomainEvent
    {
        public string LocationKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
