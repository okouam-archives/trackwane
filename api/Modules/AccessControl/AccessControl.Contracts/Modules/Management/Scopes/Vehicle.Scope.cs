using System;
using Trackwane.AccessControl.Contracts;
using Trackwane.Contracts;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Contracts.Scopes
{
    public class VehicleScope
    {
        private readonly TrackwaneClient client;

        public VehicleScope(TrackwaneClient client)
        {
            this.client = client;
        }

        const string VEHICLE_COLLECTION_URL = "/organizations/{0}/vehicles/";
        const string VEHICLE_RESOURCE_URL = VEHICLE_COLLECTION_URL + "/{{1}}";
        const string CHANGE_DRIVER_ASSIGNMENT_URL = VEHICLE_RESOURCE_URL + "/driver/{2}";
        const string CHANGE_TRACKER_ASSIGNMENT_URL = VEHICLE_RESOURCE_URL + "/tracker/{2}";
        const string DRIVER_COLLECTION_URL = "/organizations/{0}/drivers";
        const string DRIVER_RESOURCE_URL = DRIVER_COLLECTION_URL + "/{{1}}";

        public void RegisterDriver(string organizationKey, CreateDriverModel model)
        {
            client.POST(client.Expand(DRIVER_COLLECTION_URL, organizationKey), model);
        }

        public void ArchiveDriver(string organizationKey, string key)
        {
            client.DELETE(client.Expand(DRIVER_RESOURCE_URL, organizationKey, key));
        }

        public void UpdateDriver(string organizationKey, string driverKey, UpdateDriverModel model)
        {
            client.POST(client.Expand(DRIVER_COLLECTION_URL, organizationKey, driverKey), model);
        }
        
        public void Archive(string organizationKey, string vehicleKey)
        {
            client.DELETE(client.Expand(VEHICLE_RESOURCE_URL, organizationKey, vehicleKey));
        }

        public void AssignTracker(string organizationKey, string vehicleKey, string trackerKey)
        {
            client.POST(client.Expand(CHANGE_TRACKER_ASSIGNMENT_URL, organizationKey, vehicleKey, trackerKey));
        }

        public void AssignDriver(string organizationKey, string vehicleKey, string driverKey)
        {
            client.POST(client.Expand(CHANGE_DRIVER_ASSIGNMENT_URL, organizationKey, vehicleKey, driverKey));
        }

        public void Register(string organizationKey, RegisterVehicleModel model)
        {
            client.POST(client.Expand(VEHICLE_COLLECTION_URL, organizationKey), model);
        }

        public void Update(string organizationKey, string key, UpdateVehicleModel model)
        {
            client.POST(client.Expand(VEHICLE_RESOURCE_URL, organizationKey, key), model);
        }


        //    member this.FindDriverById(organizationKey: string, key: string): DriverDetails =
        //        this.GET<DriverDetails>(this.Expand(DRIVER_RESOURCE_URL, organizationKey, key))

        //    member this.FindDriverBySearchCriteria(organizationKey: string, model: SearchCriteriaModel): ResponsePage<DriverSummary> =
        //        this.GET<ResponsePage<DriverSummary>>(this.Expand(DRIVER_COLLECTION_URL, organizationKey), model)
        
        //    member this.RemoveDriverFromVehicle(organizationKey: string, vehicleKey: string, driverKey: string) =
        //        this.DELETE(this.Expand(CHANGE_DRIVER_ASSIGNMENT_URL, organizationKey, vehicleKey, driverKey))

        //    member this.RemoveTrackerFromVehicle(organizationKey: string, vehicleKey: string, trackerKey: string) =
        //        this.DELETE(this.Expand(CHANGE_TRACKER_ASSIGNMENT_URL, organizationKey, vehicleKey, trackerKey))

        //    member this.FindVehicleById(organizationKey: string, key: string): VehicleDetails =
        //        this.GET<VehicleDetails>(this.Expand(VEHICLE_RESOURCE_URL, organizationKey, key))

        //    member this.SearchVehicles(organizationKey: string, model: SearchVehiclesModel):  ResponsePage<VehicleSummary> =
        //        this.GET<ResponsePage<VehicleSummary>>(this.Expand(VEHICLE_COLLECTION_URL, organizationKey), model)

    }
}