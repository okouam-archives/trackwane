namespace Trackwane.Data.Events

open System
open Trackwane.Framework.Common
open Geo.Geometries

type SensorReadingSaved() = 
    inherit DomainEvent()
    
    member val ReadingKey : string = null with get, set
    
    member val TrackerKey : string = null with get, set
    
    member val OrganizationKey : string = null with get, set
    
    member val HardwareId : string = null with get, set
    
    member val Coordinates : Point = null with get, set
    
    member val Timestamp : Nullable<DateTime> = Nullable() with get, set
    
    member val Speed : Nullable<double> = Nullable() with get, set
    
    member val Orientation : Nullable<double> = Nullable() with get, set
    
    member val Petrol : Nullable<double> = Nullable() with get, set
    
    member val BatteryLevel : Nullable<double> = Nullable() with get, set
    
    member val Heading : Nullable<double> = Nullable() with get, set

    member val Distance : Nullable<decimal> = Nullable() with get, set

   

       

        