namespace Trackwane.Management.Contracts.Models

open Geo.Geometries

type AlertType = Speed = 0 | Petrol = 1 | Battery = 2 

//-> Alerts

[<CLIMutable>]
type UpdateAlertModel = {Name: string}

[<CLIMutable>]
type AlertSummary = {Key: string}

[<CLIMutable>]
type CreateAlertModel = {Key: string; Name: string}

[<CLIMutable>]
type SearchAlertsModel = {Name: string}

[<CLIMutable>]
type AlertDetails = {IsArchived: bool; Name: string; Threshold: int; Type: string}

//-> Boundaries

[<CLIMutable>]
type BoundaryDetails = {Coordinates: Polygon; IsArchived: bool; Name: string; Type: string}
 
[<CLIMutable>]
type BoundarySummary = {Key: string}

[<CLIMutable>]
type SearchBoundariesModel = {Name: string}

[<CLIMutable>]
type UpdateBoundaryModel = {Name: string; Coordinates: Polygon}

[<CLIMutable>]
type CreateBoundaryModel = {Key: string; Name: string; Coordinates: Polygon; Type: string}

//-> Drivers

[<CLIMutable>]
type CreateDriverModel = {Key: string; Name: string}

[<CLIMutable>]
type DriverDetails = {Name: string; IsArchived: bool; Id: string}

[<CLIMutable>]
type DriverSummary = {Key: string}
 
[<CLIMutable>]
type UpdateDriverModel = {Name: string}

[<CLIMutable>]
type SearchDriversModel = {Name: string}

//-> Locations

[<CLIMutable>]
type LocationDetails = {Name: string; IsArchived: bool; Coordinates: Point}

[<CLIMutable>]
type LocationSummary = {Key: string}

[<CLIMutable>]
type UpdateLocationModel = {Name: string; Coordinates: Point}

[<CLIMutable>]
type SearchLocationsModel = {Name: string}

[<CLIMutable>]
type RegisterLocationModel = {Key: string; Name: string; Coordinates: string}

//-> Trackers

[<CLIMutable>]
type RegisterTrackerModel = {Key: string; HardwareId: string; Model: string}

[<CLIMutable>]
type SearchCriteriaModel = {Name: string}

[<CLIMutable>]
type TrackerDetails = {Model: string; IsArchived: bool; HardwareId: string; Key: string}

[<CLIMutable>]
type TrackerSummary = {Key: string; IsArchived: bool; HardwareId: string; Model: string}

[<CLIMutable>]
type UpdateTrackerModel = {Model: string}

//-> Vehicles

[<CLIMutable>]
type RegisterVehicleModel = {Key: string; Identifier: string}

[<CLIMutable>]
type SearchVehiclesModel = {Identifier: string}

[<CLIMutable>]
type UpdateVehicleModel = {Identifier: string}

[<CLIMutable>]
type VehicleDetails = {IsArchived: bool; OrganizationId: string; DriverId: string; TrackerId: string; TrackerHarwareId: string; Identifier: string; DriverName: string}

[<CLIMutable>]
type VehicleSummary = {IsArchived: bool; OrganizationId: string; Identifier: string}
