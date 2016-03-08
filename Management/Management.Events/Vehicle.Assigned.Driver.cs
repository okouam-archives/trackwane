using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class VehicleAssignedDriver : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string VehicleKey { get; set; }

        public string DriverKey { get; set; }
    }
}
