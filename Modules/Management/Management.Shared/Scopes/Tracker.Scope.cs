using System;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Contracts.Scopes
{
    public  class TrackerScopeContext
    {
        const string TRACKER_COLLECTION_URL = "/organizations/{0}/trackers";
        const string TRACKER_RESOURCE_URL = TRACKER_COLLECTION_URL + "/{{1}}";

        private readonly ManagementContext client;

        public TrackerScopeContext(ManagementContext client)
        {
            this.client = client;
        }

        public void Register(string organizationKey, RegisterTrackerModel model)
        {
            client.POST(client.Expand(TRACKER_COLLECTION_URL, organizationKey), model);
        }

        public void Archive(string organizationKey, string trackerKey)
        {
            client.DELETE(client.Expand(TRACKER_RESOURCE_URL, organizationKey, trackerKey));
        }

        //    member this.UpdateTracker(organizationKey: string, key: string, model: UpdateTrackerModel) =
        //        this.POST(this.Expand(TRACKER_RESOURCE_URL, organizationKey, key), model)


        //    member this.FindTrackerById(organizationKey: string, key: string) : TrackerDetails =
        //        this.GET<TrackerDetails>(this.Expand(TRACKER_RESOURCE_URL, organizationKey, key))

        //    member this.FindTrackerBySearchCriteria(organizationKey: string, model: SearchCriteriaModel): ResponsePage<TrackerSummary> =
        //        this.POST<ResponsePage<TrackerSummary>>(this.Expand(TRACKER_COLLECTION_URL, organizationKey), model)

    }
}