using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class DriverUpdated : DomainEvent
    {
        public string DriverKey { get; set; }

        public string OrganizationKey { get; set; }

        public State Current { get; set; }

        public State Previous { get; set; }

        public class State
        {
            public string Name { get; set; }
        }
    }
}
