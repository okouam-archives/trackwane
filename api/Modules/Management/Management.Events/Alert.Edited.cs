using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class AlertEdited : DomainEvent
    {
        public string AlertKey { get; set; }

        public string OrganizationKey { get; set; }

        public State Previous { get; set; }

        public State Current { get; set; }

        public class State
        {
            public int Threshold { get; set; }

            public AlertType Type { get; set; }

            public string Name { get; set; }
        }
    }
}
