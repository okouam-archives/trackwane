using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Management.Models.Trackers;

namespace Trackwane.Management.Client
{
    public partial class ManagementContext
    {
        public class TrackerCommandsAndQueries
        {
            private const string COLLECTION_URL = "/organizations/{0}/trackers";
            private static readonly string RESOURCE_URL = $"{COLLECTION_URL}/{{1}}";
            private readonly RestClient restClient;

            public TrackerCommandsAndQueries(RestClient restClient)
            {
                this.restClient = restClient;
            }

            public void Archive(string organizationKey, string key) =>
                DELETE(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public void Update(string organizationKey, string key, UpdateTrackerModel model) =>
                POST(restClient, Expand(RESOURCE_URL, organizationKey, key), model);

            public void Create(string organizationKey, RegisterTrackerModel model) =>
                POST(restClient, Expand(COLLECTION_URL, organizationKey), model);

            public TrackerDetails FindById(string organizationKey, string key) => 
                GET<TrackerDetails>(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public ResponsePage<TrackerSummary> FindBySearchCriteria(string organizationKey, SearchCriteriaModel model) =>
                POST<ResponsePage<TrackerSummary>>(restClient, Expand(COLLECTION_URL, organizationKey), model);
        }
    }
}
