using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Events
{
    public class UserRegistered : DomainEvent
    {
        public string UserKey { get; set; }

        public string ParentOrganizationKey { get; set; }

        public string DisplayName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
