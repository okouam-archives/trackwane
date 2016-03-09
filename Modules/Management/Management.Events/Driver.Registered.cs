using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class DriverRegistered : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string DriverKey { get; set; }

        public string Name { get; set; }
    }
}
