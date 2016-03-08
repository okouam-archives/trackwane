using System;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Events
{
    public class NotificationAvailable : DomainEvent
    {
        public Guid EventId { get; set; }
    }
}