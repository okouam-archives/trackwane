namespace Trackwane.Management.Contracts.Client

open Trackwane.Framework.Common
open Trackwane.Framework.Common.Interfaces
open Trackwane.Framework.Client
open Trackwane.Management.Contracts.Models

type ManagementContext(baseUrl, config : IPlatformConfig) = 
    inherit ContextClient<ManagementContext>(baseUrl, config)   
    
    let BOUNDARY_COLLECTION_URL = "/organizations/{0}/boundaries"
    let BOUNDARY_RESOURCE_URL = BOUNDARY_COLLECTION_URL + "/{{1}}"

    let VEHICLE_COLLECTION_URL = "/organizations/{0}/vehicles/"
    let VEHICLE_RESOURCE_URL = VEHICLE_COLLECTION_URL + "/{{1}}"
    let CHANGE_DRIVER_ASSIGNMENT_URL = VEHICLE_RESOURCE_URL + "/driver/{2}"
    let CHANGE_TRACKER_ASSIGNMENT_URL = VEHICLE_RESOURCE_URL + "/tracker/{2}"

    let ALERT_COLLECTION_URL = "/organizations/{0}/alerts";
    let ALERT_RESOURCE_URL = ALERT_COLLECTION_URL + "/{{1}}";

    let TRACKER_COLLECTION_URL = "/organizations/{0}/trackers";
    let TRACKER_RESOURCE_URL = TRACKER_COLLECTION_URL + "/{{1}}";

    let LOCATION_COLLECTION_URL = "/organizations/{0}/locations";
    let LOCATION_RESOURCE_URL = LOCATION_COLLECTION_URL + "/{{1}}";

    let DRIVER_COLLECTION_URL = "/organizations/{0}/drivers";
    let DRIVER_RESOURCE_URL = DRIVER_COLLECTION_URL + "/{{1}}";

    //-> Boundaries

    member this.ArchiveBoundary(organizationKey: string, key: string) = this.DELETE(this.Expand(BOUNDARY_RESOURCE_URL, organizationKey, key));

    member this.UpdateBoundary(organizationKey: string, key: string, model: UpdateBoundaryModel) =
        this.POST(this.Expand(BOUNDARY_RESOURCE_URL, organizationKey, key), model);

    member this.CreateBoundary(organizationKey: string, model: CreateBoundaryModel) =
        this.POST(this.Expand(BOUNDARY_COLLECTION_URL, organizationKey), model);

    member this.FindBoundaryByKey(organizationKey: string, key: string) : BoundaryDetails =
        this.GET<BoundaryDetails>(this.Expand(BOUNDARY_RESOURCE_URL, organizationKey, key));
        
    member this.SearchBoundaries(organizationKey: string, model: SearchBoundariesModel) : ResponsePage<BoundarySummary> =
        this.GET<ResponsePage<BoundarySummary>>(this.Expand(BOUNDARY_COLLECTION_URL, organizationKey), model);

    //-> Vehicles

    member this.AssignDriverToVehicle(organizationKey: string, vehicleKey: string, driverKey: string) =
        this.POST(this.Expand(CHANGE_DRIVER_ASSIGNMENT_URL, organizationKey, vehicleKey, driverKey))

    member this.AssignTrackerToVehicle(organizationKey: string, vehicleKey: string, trackerKey: string) =
        this.POST(this.Expand(CHANGE_TRACKER_ASSIGNMENT_URL, organizationKey, vehicleKey, trackerKey))

    member this.RemoveDriverFromVehicle(organizationKey: string, vehicleKey: string, driverKey: string) =
        this.DELETE(this.Expand(CHANGE_DRIVER_ASSIGNMENT_URL, organizationKey, vehicleKey, driverKey))

    member this.RemoveTrackerFromVehicle(organizationKey: string, vehicleKey: string, trackerKey: string) =
        this.DELETE(this.Expand(CHANGE_TRACKER_ASSIGNMENT_URL, organizationKey, vehicleKey, trackerKey))

    member this.ArchiveVehicle(organizationKey: string, key: string) =
        this.DELETE(this.Expand(VEHICLE_RESOURCE_URL, organizationKey, key))

    member this.UpdateVehicle(organizationKey: string, key: string, model: UpdateVehicleModel) =
        this.POST(this.Expand(VEHICLE_RESOURCE_URL, organizationKey, key), model)

    member this.RegisterVehicle(organizationKey: string, model: RegisterVehicleModel) =
        this.POST(this.Expand(VEHICLE_COLLECTION_URL, organizationKey), model)

    member this.FindVehicleById(organizationKey: string, key: string): VehicleDetails =
        this.GET<VehicleDetails>(this.Expand(VEHICLE_RESOURCE_URL, organizationKey, key))

    member this.SearchVehicles(organizationKey: string, model: SearchVehiclesModel):  ResponsePage<VehicleSummary> =
        this.GET<ResponsePage<VehicleSummary>>(this.Expand(VEHICLE_COLLECTION_URL, organizationKey), model)

    //-> Trackers

    member this.ArchiveTracker(organizationKey: string, key: string) =
        this.DELETE(this.Expand(TRACKER_RESOURCE_URL, organizationKey, key))

    member this.UpdateTracker(organizationKey: string, key: string, model: UpdateTrackerModel) =
        this.POST(this.Expand(TRACKER_RESOURCE_URL, organizationKey, key), model)

    member this.CreateTracker(organizationKey: string, model: RegisterTrackerModel) =
        this.POST(this.Expand(TRACKER_COLLECTION_URL, organizationKey), model)

    member this.FindTrackerById(organizationKey: string, key: string) : TrackerDetails =
        this.GET<TrackerDetails>(this.Expand(TRACKER_RESOURCE_URL, organizationKey, key))

    member this.FindTrackerBySearchCriteria(organizationKey: string, model: SearchCriteriaModel): ResponsePage<TrackerSummary> =
        this.POST<ResponsePage<TrackerSummary>>(this.Expand(TRACKER_COLLECTION_URL, organizationKey), model)

    //-> Alerts

    member this.ArchiveAlert(organizationKey: string, alertKey: string) =
        this.DELETE(this.Expand(ALERT_RESOURCE_URL, organizationKey, alertKey))
           
    member this.UpdateAlert(organizationKey: string, alertKey: string, model: UpdateAlertModel) =
        this.POST(this.Expand(ALERT_RESOURCE_URL, organizationKey, alertKey), model)
           
    member this.CreateAlert(organizationKey: string, name: string, model: CreateAlertModel) =
        this.POST(this.Expand(ALERT_COLLECTION_URL, organizationKey), model);
 
    member this.FindAlertByKey(organizationKey: string, key: string): AlertDetails =
        this.GET<AlertDetails>(this.Expand(ALERT_RESOURCE_URL, organizationKey, key))

    member this.FindAlertBySearchCriteria(organizationKey: string, name: string, model: SearchCriteriaModel): ResponsePage<AlertSummary> =
        this.GET<ResponsePage<AlertSummary>>(this.Expand(ALERT_COLLECTION_URL, organizationKey), model)
            
    //-> Drivers

    member this.ArchiveDriver(organizationKey: string, key: string) =
        this.DELETE(this.Expand(DRIVER_RESOURCE_URL, organizationKey, key))

    member this.UpdateDriver(organizationKey: string, key: string, model: UpdateDriverModel) =
        this.POST(this.Expand(DRIVER_COLLECTION_URL, organizationKey, key), model)

    member this.RegisterDriver(organizationKey: string, model: CreateDriverModel) =
        this.POST(this.Expand(DRIVER_COLLECTION_URL, organizationKey), model)

    member this.FindDriverById(organizationKey: string, key: string): DriverDetails =
        this.GET<DriverDetails>(this.Expand(DRIVER_RESOURCE_URL, organizationKey, key))

    member this.FindDriverBySearchCriteria(organizationKey: string, model: SearchCriteriaModel): ResponsePage<DriverSummary> =
        this.GET<ResponsePage<DriverSummary>>(this.Expand(DRIVER_COLLECTION_URL, organizationKey), model)

    //-> Locations

    member this.ArchiveLocation(organizationKey: string, key: string) =
        this.DELETE(this.Expand(LOCATION_RESOURCE_URL, organizationKey, key));

    member this.UpdateLocation(organizationKey: string, key: string, model: UpdateLocationModel) =
        this.POST(this.Expand(LOCATION_RESOURCE_URL, organizationKey, key), model)

    member this.RegisterLocation(organizationKey: string, model: RegisterLocationModel) =
        this.POST(this.Expand(LOCATION_COLLECTION_URL, organizationKey), model)

    member this.FindByKey(organizationKey: string, key: string): LocationDetails =
        this.GET<LocationDetails>(this.Expand(LOCATION_RESOURCE_URL, organizationKey, key))

    member this.FindBySearchCriteria(organizationKey: string, model: SearchCriteriaModel): ResponsePage<LocationSummary> =
        this.POST<ResponsePage<LocationSummary>>(this.Expand(LOCATION_COLLECTION_URL, organizationKey), model)
      