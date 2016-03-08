using Trackwane.Framework.Common;

namespace Trackwane.Framework.Events
{
    public class NotificationCreated : DomainEvent
    {
        public string NotificationId { get; set; }
    }
}
