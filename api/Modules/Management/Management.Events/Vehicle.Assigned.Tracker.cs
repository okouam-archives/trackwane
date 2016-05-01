using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class VehicleAssignedTracker : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string VehicleKey { get; set; }

        public string TrackerKey { get; set; }
    }
}
