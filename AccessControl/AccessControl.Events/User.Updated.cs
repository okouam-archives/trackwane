using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Events
{
    public class UserUpdated : DomainEvent
    {
        public string UserKey { get; set; }

        public State Previous { get; set; }

        public State Current { get; set; }

        public class State
        {
            public string DisplayName { get; set; }

            public string Email { get; set; }
        }
    }
}
