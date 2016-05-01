using Trackwane.AccessControl.Contracts.Models;
using Trackwane.Contracts;

namespace Trackwane.AccessControl.Contracts.Scopes
{
    public class UserScope
    {
        private const string USER_RESOURCE_URL = "/organizations/{0}/users/{1}";
        private const string USER_COLLECTION_URL = "/organizations/{0}/users";
        private const string FIND_BY_KEY_URL = "/users/{0}";
        private const string USER_APPLICATION_COUNT_URL = "/users/count";
        private const string GET_ACCESS_TOKEN_URL = "/token?email={0}&password={1}";
        private const string USER_COUNT_URL = "/organizations/{0}/users/count";

        private readonly TrackwaneClient client;

        public UserScope(TrackwaneClient client)
        {
            this.client = client;
        }

        public void Register(string organizationKey, RegisterApplicationModel model)
        {
            client.POST(client.Expand(USER_COLLECTION_URL, organizationKey), model);
        }

        public UserDetails GetAccessToken(string username, string password)
        {
            return client.GET<UserDetails>(client.Expand(GET_ACCESS_TOKEN_URL, username, password));
        }

        public void Archive(string organizationKey, string userKey)
        {
            client.DELETE(client.Expand(USER_RESOURCE_URL, organizationKey, userKey));
        }

        public void Update(string organizationKey, string userKey, UpdateUserModel model)
        {
            client.POST(client.Expand(USER_RESOURCE_URL, organizationKey, userKey), model);
        }

        public dynamic FindByKey(string userId)
        {
            return client.GET<dynamic>(client.Expand(FIND_BY_KEY_URL, userId));
        }

        public int Count()
        {
            return client.GET<int>(client.Expand(USER_APPLICATION_COUNT_URL));
        }

        public int Count(string organizationKey)
        {
            return client.GET<int>(client.Expand(USER_COUNT_URL, organizationKey));
        }
    }
}