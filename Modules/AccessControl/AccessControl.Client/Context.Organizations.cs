using RestSharp;
using Trackwane.AccessControl.Models.Oganizations;

namespace Trackwane.AccessControl.Client
{
    public partial class AccessControlContext
    {
        public class OrganizationCommandsAndQueries
        {
            private const string RESOURCE_URL = "/organizations/{0}";
            private const string COLLECTION_URL = "/organizations";

            private readonly string GRANT_VIEW_PERMISSION_URL =  $"{RESOURCE_URL}/users/{{1}}/view";
            private readonly string REVOKE_VIEW_PERMISSION_URL = $"{RESOURCE_URL}/users/{{1}}/view";

            private readonly string GRANT_MANAGE_PERMISSION_URL = $"{RESOURCE_URL}/users/{{1}}/manage";
            private readonly string REVOKE_MANAGE_PERMISSION_URL = $"{RESOURCE_URL}/users/{{1}}/manage";

            private readonly string GRANT_ADMINISTRATE_PERMISSION_URL = $"{RESOURCE_URL}/users/{{1}}/administrate";
            private readonly string REVOKE_ADMINISTRATE_PERMISSION_URL = $"{RESOURCE_URL}/users/{{1}}/administrate";

            private readonly string COUNT_URL = $"{COLLECTION_URL}/count";

            private readonly RestClient client;

            public OrganizationCommandsAndQueries(RestClient client) 
            {
                this.client = client;
            }

            public void GrantAdministratePermission(string organizationKey, string userKey) =>
                POST(client, Expand(GRANT_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey));

            public void GrantManagePermission(string organizationKey, string userKey) =>
                POST(client, Expand(GRANT_MANAGE_PERMISSION_URL, organizationKey, userKey));
            
            public void GrantViewPermission(string organizationKey, string userKey) =>
                POST(client, Expand(GRANT_VIEW_PERMISSION_URL, organizationKey, userKey));
            
            public void RevokeAdministratePermission(string organizationKey, string userKey) =>
                DELETE(client, Expand(REVOKE_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey));

            public void RevokeViewPermission(string organizationKey, string userKey) =>
                DELETE(client, Expand(REVOKE_VIEW_PERMISSION_URL, organizationKey, userKey));

            public void RevokeManagePermission(string organizationKey, string userKey) => 
                DELETE(client, Expand(REVOKE_MANAGE_PERMISSION_URL, organizationKey, userKey));
            
            public void ArchiveOrganization(string organizationKey) =>
                DELETE(client, Expand(RESOURCE_URL, organizationKey));

            public void UpdateOrganization(string organizationId, UpdateOrganizationModel model) =>
                POST(client, Expand(RESOURCE_URL, organizationId), model);

            public void RegisterOrganization(RegisterOrganizationModel model) =>
                POST(client, Expand(COLLECTION_URL), model);

            public OrganizationDetails FindByKey(string organizationKey) =>
                GET<OrganizationDetails>(client, Expand(RESOURCE_URL, organizationKey));

            public int Count() => 
                GET<int>(client, Expand(COUNT_URL));
        }
    }
}
