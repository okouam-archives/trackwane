using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Events
{
    public class OrganizationArchived : DomainEvent
    {
        public string OrganizationKey { get; set; }
    }
}
