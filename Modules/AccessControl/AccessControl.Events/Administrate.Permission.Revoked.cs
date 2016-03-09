using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Events
{
    public class AdministratePermissionRevoked : DomainEvent
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
