using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class AlertArchived : DomainEvent
    {
        public string AlertKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
