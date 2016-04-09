using Trackwane.Framework.Common;

// ReSharper disable CheckNamespace
namespace Trackwane.AccessControl.Contracts.Events
// ReSharper restore CheckNamespace
{
    public class AdministratePermissionGranted : DomainEvent
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class AdministratePermissionRevoked : DomainEvent
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class ManagePermissionGranted : DomainEvent
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class ManagePermissionRevoked : DomainEvent
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class OrganizationArchived : DomainEvent
    {
        public string OrganizationKey { get; set; }
    }

    public class OrganizationRegistered : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string Name { get; set; }
    }

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

    public class UserArchived : DomainEvent
    {
        public string UserKey { get; set; }
    }

    public class UserRegistered : DomainEvent
    {
        public string UserKey { get; set; }

        public string ParentOrganizationKey { get; set; }

        public string DisplayName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

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

    public class ViewPermissionGranted : DomainEvent
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class ViewPermissionRevoked : DomainEvent
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }
    }
}
