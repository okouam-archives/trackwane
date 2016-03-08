using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Management.Responses.Boundaries;

namespace Trackwane.Management.Client
{
    public partial class ManagementContext
    {
        public class BoundaryCommandsAndQueries
        {
            private const string COLLECTION_URL = "/organizations/{0}/boundaries";
            private static readonly string RESOURCE_URL = $"{COLLECTION_URL}/{{1}}";
            private readonly RestClient restClient;

            public BoundaryCommandsAndQueries(RestClient restClient)
            {
                this.restClient = restClient;
            }

            public void Archive(string organizationKey, string key) =>
                DELETE(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public void Update(string organizationKey, string key, UpdateBoundaryModel model) => 
                POST(restClient, Expand(RESOURCE_URL, organizationKey, key), model);

            public void Create(string organizationKey, CreateBoundaryModel model) =>
                POST(restClient, Expand(COLLECTION_URL, organizationKey), model);

            public BoundaryDetails FindById(string organizationKey, string key) =>
                GET<BoundaryDetails>(restClient, Expand(RESOURCE_URL, organizationKey, key));
        
            public ResponsePage<BoundarySummary> FindBySearchCriteria(string organizationKey, SearchCriteriaModel model) =>
                GET<ResponsePage<BoundarySummary>>(restClient, Expand(COLLECTION_URL, organizationKey), model);
        }
    }
}
