using Trackwane.Framework.Common;

namespace Trackwane.Framework.Events
{
    public class AlertRaised : DomainEvent
    {
        public string AlertKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
