using Trackwane.AccessControl.Contracts.Models;
using Trackwane.Contracts;

namespace Trackwane.AccessControl.Contracts.Scopes
{
    public class OrganizationScope
    {
        private const string ORGANIZATION_RESOURCE_URL = "/organizations/{0}";
        private const string ORGANIZATION_COLLECTION_URL = "/organizations";
        private readonly string GRANT_VIEW_PERMISSION_URL = string.Format("{0}/users/{{1}}/view", ORGANIZATION_RESOURCE_URL);
        private readonly string REVOKE_VIEW_PERMISSION_URL = string.Format("{0}/users/{{1}}/view", ORGANIZATION_RESOURCE_URL);
        private readonly string GRANT_MANAGE_PERMISSION_URL = string.Format("{0}/users/{{1}}/manage", ORGANIZATION_RESOURCE_URL);
        private readonly string REVOKE_MANAGE_PERMISSION_URL = string.Format("{0}/users/{{1}}/manage", ORGANIZATION_RESOURCE_URL);
        private readonly string GRANT_ADMINISTRATE_PERMISSION_URL = string.Format("{0}/users/{{1}}/administrate", ORGANIZATION_RESOURCE_URL);
        private readonly string REVOKE_ADMINISTRATE_PERMISSION_URL = string.Format("{0}/users/{{1}}/administrate", ORGANIZATION_RESOURCE_URL);
        private readonly string ORGANIZATION_COUNT_URL = string.Format("{0}/count", ORGANIZATION_COLLECTION_URL);

        private readonly TrackwaneClient client;

        public OrganizationScope(TrackwaneClient client)
        {
            this.client = client;
        }

        public void GrantAdministratePermission(string organizationKey, string userKey)
        {
            client.POST(client.Expand(GRANT_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey));
        }

        public void GrantManagePermission(string organizationKey, string userKey)
        {
            client.POST(client.Expand(GRANT_MANAGE_PERMISSION_URL, organizationKey, userKey));
        }

        public void GrantViewPermission(string organizationKey, string userKey)
        {
            client.POST(client.Expand(GRANT_VIEW_PERMISSION_URL, organizationKey, userKey));
        }

        public void RevokeAdministratePermission(string organizationKey, string userKey)
        {
            client.DELETE(client.Expand(REVOKE_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey));
        }

        public void RevokeViewPermission(string organizationKey, string userKey)
        {
            client.DELETE(client.Expand(REVOKE_VIEW_PERMISSION_URL, organizationKey, userKey));
        }

        public void RevokeManagePermission(string organizationKey, string userKey)
        {
            client.DELETE(client.Expand(REVOKE_MANAGE_PERMISSION_URL, organizationKey, userKey));
        }

        public void ArchiveOrganization(string organizationKey)
        {
            client.DELETE(client.Expand(ORGANIZATION_RESOURCE_URL, organizationKey));
        }

        public void UpdateOrganization(string organizationId, UpdateOrganizationModel model)
        {
            client.POST(client.Expand(ORGANIZATION_RESOURCE_URL, organizationId), model);
        }

        public void RegisterOrganization(RegisterOrganizationModel model)
        {
            client.POST(client.Expand(ORGANIZATION_COLLECTION_URL), model);
        }

        public OrganizationDetails FindByKey(string organizationKey)
        {
            var result = client.GET<OrganizationDetails>(client.Expand(ORGANIZATION_RESOURCE_URL, organizationKey));
            return result;
        }

        public int Count()
        {
            return client.GET<int>(client.Expand(ORGANIZATION_COUNT_URL));
        }
    }
}