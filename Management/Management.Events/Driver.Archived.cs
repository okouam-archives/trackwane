using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class DriverArchived : DomainEvent
    {
        public string DriverKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
