using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Events
{
    public class OrganizationUpdated : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public State Current { get; set; }

        public State Previous { get; set; }

        public class State
        {
            public string Name { get; set; }
        }
    }
}
