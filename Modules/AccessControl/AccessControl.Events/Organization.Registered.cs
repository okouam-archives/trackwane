using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Events
{
    public class OrganizationRegistered : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string Name { get; set; }
    }
}
