using Trackwane.AccessControl.Contracts.Models;
using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.AccessControl.Contracts
{
    public class AccessControlContext : ContextClient<AccessControlContext>
    {
        public AccessControlContext(string baseUrl, IConfig config) : base(baseUrl, config)
        {

        }

        public void ArchiveOrganization(string organizationKey)
        {
            throw new System.NotImplementedException();
        }

        public void GrantAdministratePermission(string organizationKey, string userKey)
        {
            throw new System.NotImplementedException();
        }

        public void GrantManagePermission(string organizationKey, string userKey)
        {
            throw new System.NotImplementedException();
        }

        public void GrantViewPermission(string organizationKey, string userKey)
        {
            throw new System.NotImplementedException();
        }

        public void RevokeAdministratePermission(string organizationKey, string key)
        {
            throw new System.NotImplementedException();
        }

        public void RevokeViewPermission(string organizationKey, string userKey)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateOrganization(string organizationKey, UpdateOrganizationModel updateOrganizationModel)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterUser(string organizationKey, RegisterUserModel registerUserModel)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(string organizationKey, string userKey, UpdateUserModel updateUserModel)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterOrganization(RegisterOrganizationModel registerOrganizationModel)
        {
            throw new System.NotImplementedException();
        }

        public void RevokeManagePermission(string organizationKey, string key)
        {
            throw new System.NotImplementedException();
        }

        public void ArchiveUser(string organizationKey, string userKey)
        {
            throw new System.NotImplementedException();
        }
    }
}
