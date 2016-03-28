namespace Trackwane.Framework.Events

open Trackwane.Framework.Common
open System

type AlertType = Speed = 0 | Petrol = 1 | Battery = 2 

type AlertRaised() = 
    inherit DomainEvent()
    member val AlertKey : string = null with get, set
    member val OrganizationKey : string = null with get, set

type ApiKeyGenerated() =
    inherit DomainEvent() 
    member val UserKey : string = null with get, set
    member val ApiKey : string = null with get, set
    
type BoundaryExited() =
    inherit DomainEvent()
    member val BoundaryKey : string = null with get, set
    member val TrackerKey : string = null with get, set
    member val OrganizationKey : string = null with get, set

type LocationDeparted() =
    inherit DomainEvent()
    member val LocationKey : string = null with get, set
    member val OrganizationKey : string = null with get, set
    member val TrackerKey : string = null with get, set

type SubscriberRegistered() = 
    inherit DomainEvent()

type NotificationAvailable() =
    inherit DomainEvent()
    member val EventId : Nullable<Guid> = Nullable() with get, set
    
type NotificationCreated() =
    inherit DomainEvent()
    member val NotificationId : string = null with get, set
    
type BoundaryEntered() =
    inherit DomainEvent()
    member val BoundaryKey = null with get, set
    member val OrganizationKey = null with get, set
    member val TrackerKey = null with get, set
    
type LocationVisited() =
    inherit DomainEvent()
    member val LocationKey = null with get, set
    member val OrganizationKey = null with get, set
    member val TrackerKey = null with get, set