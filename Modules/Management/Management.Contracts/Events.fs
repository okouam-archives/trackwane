namespace Trackwane.Management.Contracts.Events

open Trackwane.Framework.Common
open Trackwane.Management.Contracts.Models
open System
open Geo.Geometries

type VehicleArchived()  = 
    inherit DomainEvent()

    member val VehicleKey: string = null with get, set

    member val OrganizationKey : string  = null with get, set
    
type LocationArchived()  = 
    inherit DomainEvent()

    member val  LocationKey : string  = null with get, set

    member val  OrganizationKey : string  = null with get, set
    
type BoundaryCreated() = 
    inherit DomainEvent()

    member val BoundaryKey : string  = null with get, set

    member val Name : string  = null with get, set
    
    member val OrganizationKey: string   = null with get, set

    member val Coordinates : Polygon  = null with get, set
    
type BoundaryArchived() = 
    inherit DomainEvent()

    member val BoundaryKey : string  = null with get, set

    member val OrganizationKey : string  = null with get, set
    

type AlertArchived(alertKey, organizationKey) = 
    inherit DomainEvent()

    member val AlertKey: string = alertKey 

    member val OrganizationKey: string = organizationKey 

type AlertCreated() =
    inherit DomainEvent()
    
    member val AlertKey : string = null with get, set

    member val Name : string = null with get, set

    member val  Type: AlertType = AlertType.Speed with get, set

    member val Threshold : int = 0 with get, set

    member val OrganizationKey : string = null with get, set
    
type DriverArchived() =
    inherit DomainEvent()

    member val DriverKey : string = null with get, set

    member val OrganizationKey : string = null with get, set
    
type DriverRegistered() =
    inherit DomainEvent()

    member val OrganizationKey : string = null with get, set

    member val DriverKey : string = null with get, set

    member val Name : string = null with get, set

type VehicleRegistered() =
    inherit DomainEvent()
    
    member val VehicleKey : string = null with get, set

    member val OrganizationKey: string = null with get, set

    member val Identifier: string = null with get, set

type TrackerUpdatedState = {
    Coordinates: Point
    Distance: Nullable<decimal>
    BatteryLevel: Nullable<double> 
    Petrol: Nullable<Double>
    Heading: Nullable<Double>
    Speed: Nullable<Double> 
    Orientation: Nullable<Double>
}

type TrackerUpdated() =
    inherit DomainEvent()

    member val OrganizationKey  : string = null with get, set

    member val TrackerKey  : string = null with get, set

    member val Previous  : TrackerUpdatedState = Unchecked.defaultof<TrackerUpdatedState> with get, set

    member val Current : TrackerUpdatedState = Unchecked.defaultof<TrackerUpdatedState> with get, set

type LocationUpdatedState = {Name: string; Coordinates: Point}

type LocationUpdated() =
    inherit DomainEvent()

    member val Current : LocationUpdatedState = Unchecked.defaultof<LocationUpdatedState> with get, set

    member val Previous  : LocationUpdatedState = Unchecked.defaultof<LocationUpdatedState> with get, set

    member val OrganizationKey  : string = null with get, set

    member val LocationKey  : string = null with get, set

    member val Name : string = null with get, set

type VehicleUpdatedState = {Identifier: string}

type VehicleUpdated() =
    inherit DomainEvent()

    member val OrganizationKey: string = null with get, set

    member val VehicleKey: string = null with get, set

    member val Previous: VehicleUpdatedState = Unchecked.defaultof<VehicleUpdatedState> with get, set

    member val Current: VehicleUpdatedState = Unchecked.defaultof<VehicleUpdatedState> with get, set

type DriverUpdatedState = {Name: string}

type DriverUpdated() =
    inherit DomainEvent()

    member val DriverKey: string = null with get, set

    member val OrganizationKey: string = null with get, set

    member val Current: DriverUpdatedState = Unchecked.defaultof<DriverUpdatedState> with get, set

    member val Previous: DriverUpdatedState = Unchecked.defaultof<DriverUpdatedState> with get, set

type BoundaryUpdatedState = {Name: string; Coordinates: Polygon}

type BoundaryUpdated() =
    inherit DomainEvent()

    member val OrganizationKey: string = null with get, set

    member val BoundaryKey: string = null with get, set

    member val Current: BoundaryUpdatedState = Unchecked.defaultof<BoundaryUpdatedState> with get, set

    member val Previous: BoundaryUpdatedState = Unchecked.defaultof<BoundaryUpdatedState> with get, set

type VehicleAssignedDriver() =
    inherit DomainEvent()

    member val OrganizationKey: string = null with get, set

    member val VehicleKey: string = null with get, set

    member val DriverKey: string = null with get, set
    

type AlertEditedState = {Threshold: int; Type: AlertType; Name: string}

type AlertEdited() =
    inherit DomainEvent()

    member val AlertKey: string = null with get, set

    member val OrganizationKey: string = null with get, set

    member val Previous: AlertEditedState = Unchecked.defaultof<AlertEditedState> with get, set

    member val Current: AlertEditedState = Unchecked.defaultof<AlertEditedState> with get, set

type VehicleAssignedTracker() =
    inherit DomainEvent()

    member val OrganizationKey: string = null with get, set

    member val VehicleKey: string = null with get, set

    member val TrackerKey: string = null with get, set
    
type LocationRegistered() =
    inherit DomainEvent()

    member val OrganizationKey: string = null with get, set

    member val LocationKey: string = null with get, set

    member val Name: string = null with get, set

    member val Coordinates: Point = null with get, set

type TrackerArchived() =
    inherit DomainEvent()

    member val TrackerKey: string = null with get, set

    member val OrganizationKey: string = null with get, set
    
type TrackerRegistered() =
    inherit DomainEvent()

    member val TrackerKey: string = null with get, set

    member val OrganizationKey: string = null with get, set
    
    member val Identifier: string = null with get, set
    
    member val Model: string = null with get, set

    member val HardwareId: string = null with get, set
    
    
    