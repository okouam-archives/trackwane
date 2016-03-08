using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class BoundaryArchived : DomainEvent
    {
        public string BoundaryKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
