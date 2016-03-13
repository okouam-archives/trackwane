using RestSharp;
using Trackwane.AccessControl.Models.Users;

namespace Trackwane.AccessControl.Client
{
    public partial class AccessControlContext
    {
        public class UserCommandsAndQueries
        {
            private const string RESOURCE_URL = "/organizations/{0}/users/{1}";
            private const string COLLECTION_URL = "/organizations/{0}/users";
            private const string FIND_BY_KEY_URL = "/users/{0}";
            private const string GET_ACCESS_TOKEN_URL = "/token?username={0}&password={1}";
            private const string CREATE_ROOT_USER_URL = "/root";

            private readonly RestClient restClient;

            public UserCommandsAndQueries(RestClient restClient)
            {
                this.restClient = restClient;
            }

            public UserDetails CreateRootUser(RegisterUserModel model) => 
                POST<UserDetails>(restClient, Expand(CREATE_ROOT_USER_URL), model);

            public void RegisterUser(string organizationKey, RegisterUserModel model) =>
                POST(restClient, Expand(COLLECTION_URL, organizationKey), model);
 
            public UserDetails GetAccessToken(string username, string password) =>
                GET<UserDetails>(restClient, Expand(GET_ACCESS_TOKEN_URL, username, password));

            public void ArchiveUser(string organizationKey, string userKey) =>
                DELETE(restClient, Expand(RESOURCE_URL, organizationKey, userKey));

            public void UpdateUser(string organizationKey, string userKey, UpdateUserModel model) =>
                POST(restClient, Expand(RESOURCE_URL, organizationKey, userKey), model);

            public UserDetails FindById(string userId) =>
                GET<UserDetails>(restClient, Expand(FIND_BY_KEY_URL, userId));
        }
    }
}
