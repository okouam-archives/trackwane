using Trackwane.Framework.Common;

namespace Trackwane.Framework.Events
{
    public class ApiKeyGenerated : DomainEvent
    {
        public string UserKey { get; set; }

        public string ApiKey { get; set; }
    }
}
