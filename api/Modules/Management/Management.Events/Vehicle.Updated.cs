using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class VehicleUpdated : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string VehicleKey { get; set; }

        public State Previous { get; set; }

        public State Current { get; set; }

        public class State
        {
            public string Identifier { get; set; }
        }
    }
}
