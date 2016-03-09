using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Management.Models.Locations;

namespace Trackwane.Management.Client
{
    public partial class ManagementContext
    {
        public class LocationCommandsAndQueries
        {
            private const string COLLECTION_URL = "/organizations/{0}/locations";
            private static readonly string RESOURCE_URL = $"{COLLECTION_URL}/{{1}}";
  
            private readonly RestClient restClient;

            public LocationCommandsAndQueries(RestClient restClient)
            {
                this.restClient = restClient;
            }

            public void Archive(string organizationKey, string key) =>
                DELETE(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public void Update(string organizationKey, string key, UpdateLocationModel model) =>
                POST(restClient, Expand(RESOURCE_URL, organizationKey, key), model);

            public void Register(string organizationKey, RegisterLocationModel model) =>
                POST(restClient, Expand(COLLECTION_URL, organizationKey), model);

            public LocationDetails FindByKey(string organizationKey, string key) =>
                GET<LocationDetails>(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public ResponsePage<LocationSummary> FindBySearchCriteria(string organizationKey, SearchCriteriaModel model) =>
                POST<ResponsePage<LocationSummary>>(restClient, Expand(COLLECTION_URL, organizationKey), model);
        }
    }
}
