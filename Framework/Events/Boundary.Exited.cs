using Trackwane.Framework.Common;

namespace Trackwane.Framework.Events
{
    public class BoundaryExited : DomainEvent
    {
        public string BoundaryKey { get; set; }

        public string TrackerKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
