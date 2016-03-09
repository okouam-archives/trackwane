using RestSharp;
using Trackwane.Framework.Common;
using Trackwane.Management.Models.Boundaries;
using Trackwane.Management.Models.Vehicles;
using SearchCriteriaModel = Trackwane.Management.Models.Vehicles.SearchCriteriaModel;

namespace Trackwane.Management.Client
{
    public partial class ManagementContext
    {
        public class VehicleCommandsAndQueries
        {
            private const string COLLECTION_URL = "/organizations/{0}/vehicles/";
            private static readonly string RESOURCE_URL = $"{COLLECTION_URL}/{{1}}";
            private static readonly string CHANGE_DRIVER_ASSIGNMENT_URL = $"{RESOURCE_URL}/driver/{2}";
            private static readonly string CHANGE_TRACKER_ASSIGNMENT_URL = $"{RESOURCE_URL}/tracker/{2}";
            private readonly RestClient restClient;

            public VehicleCommandsAndQueries(RestClient restClient)
            {
                this.restClient = restClient;
            }

            public void AssignDriverToVehicle(string organizationKey, string vehicleKey, string driverKey) =>
                POST(restClient, Expand(CHANGE_DRIVER_ASSIGNMENT_URL, organizationKey, vehicleKey, driverKey));

            public void AssignTrackerToVehicle(string organizationKey, string vehicleKey, string trackerKey) =>
              POST(restClient, Expand(CHANGE_TRACKER_ASSIGNMENT_URL, organizationKey, vehicleKey, trackerKey));

            public void RemoveDriverFromVehicle(string organizationKey, string vehicleKey, string driverKey) =>
              DELETE(restClient, Expand(CHANGE_DRIVER_ASSIGNMENT_URL, organizationKey, vehicleKey, driverKey));

            public void RemoveTrackerFromVehicle(string organizationKey, string vehicleKey, string trackerKey) =>
              DELETE(restClient, Expand(CHANGE_TRACKER_ASSIGNMENT_URL, organizationKey, vehicleKey, trackerKey));

            public void Archive(string organizationKey, string key) =>
                DELETE(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public void Update(string organizationKey, string key, UpdateVehicleModel model) =>
                POST(restClient, Expand(RESOURCE_URL, organizationKey, key), model);

            public void Register(string organizationKey, RegisterVehicleModel model) => 
                POST(restClient, Expand(COLLECTION_URL, organizationKey), model);

            public BoundaryDetails FindById(string organizationKey, string key) =>
                GET<BoundaryDetails>(restClient, Expand(RESOURCE_URL, organizationKey, key));

            public ResponsePage<BoundarySummary> FindBySearchCriteria(string organizationKey, SearchCriteriaModel model) =>
                GET<ResponsePage<BoundarySummary>>(restClient, Expand(COLLECTION_URL, organizationKey), model);
        }
    }
}
