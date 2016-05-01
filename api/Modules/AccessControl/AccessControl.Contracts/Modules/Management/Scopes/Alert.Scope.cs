using Trackwane.AccessControl.Contracts;
using Trackwane.Contracts;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Contracts.Scopes
{
    public class AlertScope
    {
        const string ALERT_COLLECTION_URL = "/organizations/{0}/alerts";
        const string ALERT_RESOURCE_URL = ALERT_COLLECTION_URL + "/{{1}}";

        private readonly TrackwaneClient client;

        public AlertScope(TrackwaneClient client)
        {
            this.client = client;
        }

        public void Archive(string organizationKey, string alertKey)
        {
            client.DELETE(client.Expand(ALERT_RESOURCE_URL, organizationKey, alertKey));
        }


        public void Update(string organizationKey, string alertKey, UpdateAlertModel model)
        {
            client.POST(client.Expand(ALERT_RESOURCE_URL, organizationKey, alertKey), model);
        }

        public void Create(string organizationKey, string name, CreateAlertModel model)
        {
            client.POST(client.Expand(ALERT_COLLECTION_URL, organizationKey), model);
        }

        //member this.FindAlertByKey(organizationKey: string, key: string): AlertDetails =
        //        this.GET<AlertDetails>(this.Expand(ALERT_RESOURCE_URL, organizationKey, key))

        //    member this.FindAlertBySearchCriteria(organizationKey: string, name: string, model: SearchCriteriaModel): ResponsePage<AlertSummary> =
        //        this.GET<ResponsePage<AlertSummary>>(this.Expand(ALERT_COLLECTION_URL, organizationKey), model)



    }
}