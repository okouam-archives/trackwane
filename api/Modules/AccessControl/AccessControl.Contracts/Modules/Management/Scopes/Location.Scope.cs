using System;
using Trackwane.AccessControl.Contracts;
using Trackwane.Contracts;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Contracts.Scopes
{
    public class LocationScope
    {
        const string LOCATION_COLLECTION_URL = "/organizations/{0}/locations";
        const string LOCATION_RESOURCE_URL = LOCATION_COLLECTION_URL + "/{{1}}";

        private readonly TrackwaneClient client;

        public LocationScope(TrackwaneClient client)
        {
            this.client = client;
        }
        
        //    member this.FindByKey(organizationKey: string, key: string): LocationDetails =
        //        this.GET<LocationDetails>(this.Expand(LOCATION_RESOURCE_URL, organizationKey, key))

        //    member this.FindBySearchCriteria(organizationKey: string, model: SearchCriteriaModel): ResponsePage<LocationSummary> =
        //        this.POST<ResponsePage<LocationSummary>>(this.Expand(LOCATION_COLLECTION_URL, organizationKey), model)

        public void Archive(string organizationKey, string key)
        {
            client.DELETE(client.Expand(LOCATION_RESOURCE_URL, organizationKey, key));
        }

        public void Register(string organizationKey, RegisterLocationModel model)
        {
            client.POST(client.Expand(LOCATION_COLLECTION_URL, organizationKey), model);
        }

        public void Update(string organizationKey, string locationKey, UpdateLocationModel model)
        {
            client.POST(client.Expand(LOCATION_RESOURCE_URL, organizationKey, locationKey), model);
        }
    }
}