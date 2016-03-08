using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class VehicleArchived : DomainEvent
    {
        public string VehicleKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
