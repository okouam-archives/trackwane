using Trackwane.AccessControl.Contracts.Models;
using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.AccessControl.Contracts
{
    public class AccessControlContext : ContextClient<AccessControlContext>
    {
        public AccessControlContext(string baseUrl, IPlatformConfig config, string applicationKey) : base(baseUrl, config, applicationKey)
        {

        }

        private const string ORGANIZATION_RESOURCE_URL = "/organizations/{0}";
        private const string ORGANIZATION_COLLECTION_URL = "/organizations";
        private readonly string GRANT_VIEW_PERMISSION_URL = string.Format("{0}/users/{{1}}/view", ORGANIZATION_RESOURCE_URL);
        private readonly string REVOKE_VIEW_PERMISSION_URL = string.Format("{0}/users/{{1}}/view", ORGANIZATION_RESOURCE_URL);
        private readonly string GRANT_MANAGE_PERMISSION_URL = string.Format("{0}/users/{{1}}/manage", ORGANIZATION_RESOURCE_URL);
        private readonly string REVOKE_MANAGE_PERMISSION_URL = string.Format("{0}/users/{{1}}/manage", ORGANIZATION_RESOURCE_URL);
        private readonly string GRANT_ADMINISTRATE_PERMISSION_URL = string.Format("{0}/users/{{1}}/administrate", ORGANIZATION_RESOURCE_URL);
        private readonly string REVOKE_ADMINISTRATE_PERMISSION_URL = string.Format("{0}/users/{{1}}/administrate", ORGANIZATION_RESOURCE_URL);
        private readonly string ORGANIZATION_COUNT_URL = string.Format("{0}/count", ORGANIZATION_COLLECTION_URL);

        private const string USER_RESOURCE_URL = "/organizations/{0}/users/{1}";
        private const string USER_COLLECTION_URL = "/organizations/{0}/users";
        private const string FIND_BY_KEY_URL = "/users/{0}";
        private const string GET_ACCESS_TOKEN_URL = "/token?username={0}&password={1}";
        private const string CREATE_ROOT_USER_URL = "/root";

        public UserDetails CreateRootUser(RegisterApplicationModel model)
        {
            return POST<UserDetails>(Expand(CREATE_ROOT_USER_URL), model);
        }

        public void RegisterUser(string organizationKey, RegisterApplicationModel model)
        {
            POST(Expand(USER_COLLECTION_URL, organizationKey), model);
        }

        public UserDetails GetAccessToken(string username, string password)
        {
            return GET<UserDetails>(Expand(GET_ACCESS_TOKEN_URL, username, password));
        }

        public void ArchiveUser(string organizationKey, string userKey)
        {
            DELETE(Expand(USER_RESOURCE_URL, organizationKey, userKey));
        }

        public void UpdateUser(string organizationKey, string userKey, UpdateUserModel model)
        {
            POST(Expand(USER_RESOURCE_URL, organizationKey, userKey), model);
        }

        public UserDetails FindById(string userId)
        {
            return GET<UserDetails>(Expand(FIND_BY_KEY_URL, userId));
        }

        public void GrantAdministratePermission(string organizationKey, string userKey)
        {
            POST(Expand(GRANT_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey));
        }

        public void GrantManagePermission(string organizationKey, string userKey)
        {
            POST(Expand(GRANT_MANAGE_PERMISSION_URL, organizationKey, userKey));
        }

        public void GrantViewPermission(string organizationKey, string userKey)
        {
            POST(Expand(GRANT_VIEW_PERMISSION_URL, organizationKey, userKey));
        }

        public void RevokeAdministratePermission(string organizationKey, string userKey)
        {
            DELETE(Expand(REVOKE_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey));
        }

        public void RevokeViewPermission(string organizationKey, string userKey)
        {
            DELETE(Expand(REVOKE_VIEW_PERMISSION_URL, organizationKey, userKey));
        }

        public void RevokeManagePermission(string organizationKey, string userKey)
        {
            DELETE(Expand(REVOKE_MANAGE_PERMISSION_URL, organizationKey, userKey));
        }

        public void ArchiveOrganization(string organizationKey)
        {
            DELETE(Expand(ORGANIZATION_RESOURCE_URL, organizationKey));
        }

        public void UpdateOrganization(string organizationId, UpdateOrganizationModel model)
        {
            POST(Expand(ORGANIZATION_RESOURCE_URL, organizationId), model);
        }

        public void RegisterOrganization(RegisterOrganizationModel model)
        {
            POST(Expand(ORGANIZATION_COLLECTION_URL), model);
        }

        public OrganizationDetails FindByKey(string organizationKey)
        {
            return GET<OrganizationDetails>(Expand(ORGANIZATION_RESOURCE_URL, organizationKey));
        }

        public int Count()
        {
            return GET<int>(Expand(ORGANIZATION_COUNT_URL));
        }
    }
}
