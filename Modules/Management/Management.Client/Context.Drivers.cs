using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Management.Models.Drivers;

namespace Trackwane.Management.Client
{
    public partial class ManagementContext
    {
        public class DriverCommandsAndQueries
        {
            private const string COLLECTION_URL = "/organizations/{0}/drivers";
            private static readonly string RESOURCE_URL = $"{COLLECTION_URL}/{{1}}";
            private readonly RestClient restClient;

            public DriverCommandsAndQueries(RestClient restClient)
            {
                this.restClient = restClient;
            }

            public void Archive(string organizationKey, string key) => 
                DELETE(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public void Update(string organizationKey, string key, UpdateDriverModel model) =>
                POST(restClient, Expand(COLLECTION_URL, organizationKey, key), model);

            public void Register(string organizationKey, CreateDriverModel model) =>
                POST(restClient, Expand(COLLECTION_URL, organizationKey), model);

            public DriverDetails FindById(string organizationKey, string key) =>
                GET<DriverDetails>(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public ResponsePage<DriverSummary> FindBySearchCriteria(string organizationKey, SearchCriteriaModel model) => 
                GET<ResponsePage<DriverSummary>>(restClient, Expand(COLLECTION_URL, organizationKey), model);
        }
    }
}
