using System;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Contracts.Scopes
{
    public class BoundaryScope
    {
        const string BOUNDARY_COLLECTION_URL = "/organizations/{0}/boundaries";
        const string BOUNDARY_RESOURCE_URL = BOUNDARY_COLLECTION_URL + "/{{1}}";

        private readonly ManagementContext client;

        public BoundaryScope(ManagementContext client)
        {
            this.client = client;
        }

        public void ArchiveBoundary(string organizationKey, string key)
        {
            client.DELETE(client.Expand(BOUNDARY_RESOURCE_URL, organizationKey, key));
        }

        public void Update(string organizationKey, string key, UpdateBoundaryModel model)
        {
            client.POST(client.Expand(BOUNDARY_RESOURCE_URL, organizationKey, key), model);
        }

        public void Create(string organizationKey, CreateBoundaryModel model)
        {
            client.POST(client.Expand(BOUNDARY_COLLECTION_URL, organizationKey), model);
        }
        
        //member this.FindBoundaryByKey(organizationKey: string, key: string) : BoundaryDetails =
        //        this.GET<BoundaryDetails>(this.Expand(BOUNDARY_RESOURCE_URL, organizationKey, key));

        //member this.SearchBoundaries(organizationKey: string, model: SearchBoundariesModel) : ResponsePage<BoundarySummary> =
        //        this.GET<ResponsePage<BoundarySummary>>(this.Expand(BOUNDARY_COLLECTION_URL, organizationKey), model);
        
    }
}