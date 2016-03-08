using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Management.Responses.Alerts;

namespace Trackwane.Management.Client
{
    public partial class ManagementContext
    {
        public class AlertCommandsAndQueries
        {
            private const string COLLECTION_URL = "/organizations/{0}/alerts";
            private static readonly string RESOURCE_URL = $"{COLLECTION_URL}/{{1}}";
            private readonly RestClient client;

            public AlertCommandsAndQueries(RestClient client)
            {
                this.client = client;
            }

            public void Archive(string organizationKey, string alertKey) =>
                DELETE(client, Expand(RESOURCE_URL, organizationKey, alertKey));
            
            public void Update(string organizationKey, string alertKey, UpdateAlertModel model) =>
                POST(client, Expand(RESOURCE_URL, organizationKey, alertKey), model);
            
            public void Create(string organizationKey, string name, CreateAlertModel model) =>
                POST(client, Expand(COLLECTION_URL, organizationKey), model);
   
            public AlertDetails FindByKey(string organizationKey, string key) =>
                GET<AlertDetails>(client, Expand(RESOURCE_URL, organizationKey, key));

            public ResponsePage<AlertSummary> FindBySearchCriteria(string organizationKey, string name, SearchCriteriaModel model) =>
                GET<ResponsePage<AlertSummary>>(client, Expand(COLLECTION_URL, organizationKey), model);
            
        }
    }
}
