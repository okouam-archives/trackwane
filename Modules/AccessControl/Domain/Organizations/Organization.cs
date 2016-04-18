using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common;
using AdministratePermissionGranted = Trackwane.AccessControl.Contracts.Events.AdministratePermissionGranted;
using AdministratePermissionRevoked = Trackwane.AccessControl.Contracts.Events.AdministratePermissionRevoked;
using ManagePermissionGranted = Trackwane.AccessControl.Contracts.Events.ManagePermissionGranted;
using ManagePermissionRevoked = Trackwane.AccessControl.Contracts.Events.ManagePermissionRevoked;

namespace Trackwane.AccessControl.Domain.Organizations
{
    public class Organization : AggregateRoot
    {
        /* Public */

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public bool IsArchived { get; set; }

        public void Update(string name)
        {
            Causes(new OrganizationUpdated {OrganizationKey = Key, Previous = new OrganizationUpdated.State {Name = Name}, Current = new OrganizationUpdated.State {Name = name}});
        }

        public Organization()
        {
            Viewers = new List<string>();
            Managers = new List<string>();
            Administrators = new List<string>();
        }

        public Organization(string applicationKey, string id, string name)
        {
            Viewers = new List<string>();
            Managers = new List<string>();
            Administrators = new List<string>();
            Causes(new OrganizationRegistered { ApplicationKey = applicationKey, Name = name, OrganizationKey = id});
        }

        public void Archive()
        {
            Causes(new OrganizationArchived {OrganizationKey = Key});
        }

        public bool CanAdministrate(User user)
        {
            return user.Role == Role.SystemManager || Administrators.Contains(user.Key);
        }

        public bool CanManage(User user)
        {
            return CanAdministrate(user) || Managers.Contains(user.Key);
        }

        public bool CanView(User user)
        {
            return CanManage(user) || Viewers.Contains(user.Key);
        }

        public void GrantAdministratePermission(string userKey)
        {
            RevokeViewPermission(userKey);

            RevokeManagePermission(userKey);

            if (!Administrators.Contains(userKey))
            {
                Causes(new AdministratePermissionGranted
                {
                    OrganizationKey = Key,
                    UserKey = userKey
                });
            }
        }

        public void RevokeAdministratePermission(string userId)
        {
            Causes(new AdministratePermissionRevoked
            {
                UserKey = userId,
                OrganizationKey = Key
            });
        }
        
        public void GrantManagePermission(string userKey)
        {
            RevokeViewPermission(userKey);

            RevokeAdministratePermission(userKey);

            if (!Managers.Contains(userKey))
            {
                Causes(new ManagePermissionGranted
                {
                    OrganizationKey = Key,
                    UserKey = userKey
                });
            }
        }

        public void RevokeManagePermission(string userId)
        {
            Causes(new ManagePermissionRevoked
            {
                UserKey = userId,
                OrganizationKey = Key
            });
        }

        public void GrantViewPermission(string userKey)
        {
            RevokeAdministratePermission(userKey);

            RevokeManagePermission(userKey);

            if (!Viewers.Contains(userKey))
            {
                Causes(new ViewPermissionGranted
                {
                    OrganizationKey = Key,
                    UserKey = userKey
                });
            }
        }

        public void RevokeViewPermission(string userId)
        {
            Causes(new ViewPermissionRevoked
            {
                OrganizationKey = Key,
                UserKey = userId
            });
        }

        public IEnumerable<string> GetViewers()
        {
            return Viewers;
        }

        public IEnumerable<string> GetAdministrators()
        {
            return Administrators;
        }

        public IEnumerable<string> GetManagers()
        {
            return Managers;
        }

        /* Protected */
        
        protected override void Apply(DomainEvent evt)
        {
            When((dynamic)evt);
        }

        /* Private */

        [JsonProperty]
        private IList<string> Managers { get; set; }

        [JsonProperty]
        private IList<string> Viewers { get; set; }

        [JsonProperty]
        private IList<string> Administrators { get; set; }

        private void When(OrganizationRegistered evt)
        {
            Name = evt.Name;
            ApplicationKey = evt.ApplicationKey;
            Key = evt.OrganizationKey;
        }

        private void When(OrganizationArchived evt)
        {
            IsArchived = true;
        }

        private void When(OrganizationUpdated evt)
        {
            Name = evt.Current.Name;
        }

        private void When(ViewPermissionRevoked evt)
        {
            RevokePermission(Viewers, evt.UserKey);
        }

        private void When(ManagePermissionRevoked evt)
        {
            RevokePermission(Managers, evt.UserKey);
        }

        private void When(AdministratePermissionRevoked evt)
        {
            RevokePermission(Administrators, evt.UserKey);
        }

        private void When(ViewPermissionGranted evt)
        {
            GrantPermission(Viewers, evt.UserKey);
        }

        private void When(ManagePermissionGranted evt)
        {
            GrantPermission(Managers, evt.UserKey);
        }

        private void When(AdministratePermissionGranted evt)
        {
            GrantPermission(Administrators, evt.UserKey);
        }
        
        private static void GrantPermission(ICollection<string> userList, string userKey)
        {
            if (!userList.Contains(userKey))
            {
                userList.Add(userKey);
            }
        }

        private static void RevokePermission(ICollection<string> userList, string userKey)
        {
            if (userList.Contains(userKey))
            {
                userList.Remove(userKey);
            }
        }
    }
}
