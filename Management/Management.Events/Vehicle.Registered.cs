using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class VehicleRegistered : DomainEvent
    {
        public string VehicleKey { get; set; }

        public string OrganizationKey { get; set; }

        public string Identifier { get; set; }
    }
}
