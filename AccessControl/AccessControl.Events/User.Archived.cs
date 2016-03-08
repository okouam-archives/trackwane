using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Events
{
    public class UserArchived : DomainEvent
    {
        public string UserKey { get; set; }
    }
}
