using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class AlertCreated : DomainEvent
    {
        public string AlertKey { get; set; }

        public string Name { get; set; }

        public AlertType Type { get; set; }

        public int Threshold { get; set; }

        public string OrganizationKey { get; set; }
    }
}
