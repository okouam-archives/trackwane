using System;
using Trackwane.AccessControl.Events;
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

        public void Update(string displayName)
        {
            Causes(new UserUpdated
            {
                UserKey = Key,
                Previous = new UserUpdated.State
                {
                      DisplayName = DisplayName,
                      Email = Email,
                },
                Current = new UserUpdated.State
                {
                    DisplayName = displayName,
                    Email = Email
                }
              
            });
        }

        public User()
        {
        }

        public User(string organizationKey, string userKey, string displayName, string email, Role role, string password)
        {
            Causes(new UserRegistered
            {
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
        }
    }
}
