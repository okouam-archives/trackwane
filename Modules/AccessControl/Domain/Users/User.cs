using System;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Domain.Users
{
    public class User : AggregateRoot
    {
        /* Public */

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public bool IsArchived { get; set; }

        public Role Role { get; set; }

        public Credentials Credentials { get; set; }

        public string Mobile { get; set; }

        public string ParentOrganizationKey { get; set; }

        protected override void Apply(DomainEvent evt)
        {
            When((dynamic) evt);
        }

        public void Archive()
        {
            Causes(new UserArchived
            {
                UserKey = Key
            });
        }

        public void UpdateEmail(string email)
        {
            Causes(new UserUpdated
            {
                UserKey = Key,
                Previous = new UserUpdated.State { Email = Email, DisplayName = DisplayName },
                Current = new UserUpdated.State { Email = email, DisplayName = DisplayName }
            });
        }

        public void UpdatePassword(string password)
        {
            Causes(new UserUpdated
            {
                UserKey = Key,
                Previous = new UserUpdated.State { Email = Email, DisplayName = DisplayName },
                Current = new UserUpdated.State { Email = Email, DisplayName = DisplayName, Password = password}
            });
        }

        public void UpdateDisplayName(string displayName)
        {
            Causes(new UserUpdated
            {
                UserKey = Key,
                Previous = new UserUpdated.State { Email = Email, DisplayName = DisplayName },
                Current = new UserUpdated.State { Email = Email, DisplayName = displayName }
            });
        }

        public User()
        {
        }

        public User(string appKey, string organizationKey, string userKey, string displayName, string email, Role role, string password)
        {
            Causes(new UserRegistered
            {
                ApplicationKey = appKey,
                ParentOrganizationKey = organizationKey,
                DisplayName = displayName,
                UserKey = userKey,
                Role = role.ToString(),
                Email = email,
                Password = password
            });
        }

        /* Private */

        private void When(UserRegistered evt)
        {
            ParentOrganizationKey = evt.ParentOrganizationKey;
            Key = evt.UserKey;
            ApplicationKey = evt.ApplicationKey;
            DisplayName = evt.DisplayName;
            Email = evt.Email;
            Role = (Role) Enum.Parse(typeof (Role), evt.Role);
            Credentials = Credentials.Create(evt.Password);
        }

        private void When(UserArchived evt)
        {
            IsArchived = true;
        }

        private void When(UserUpdated evt)
        {
            DisplayName = evt.Current.DisplayName;
            if (!string.IsNullOrEmpty(evt.Current.Password))
            {
                Credentials = Credentials.Create(evt.Current.Password);
            }
            Email = evt.Current.Email;
        }
    }
}
