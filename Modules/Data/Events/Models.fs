namespace Trackwane.Data.Contracts.Models

open System
open Trackwane.Framework.Common.Interfaces

type SaveSensorReadingModel() = 
    
    member val Latitude : Nullable<double> =  Nullable() with get, set

    member val Longitude : Nullable<double> =  Nullable() with get, set

    member val Heading : Nullable<double> =  Nullable() with get, set

    member val Orientation : Nullable<int> = Nullable() with get, set

    member val Speed : Nullable<int> = Nullable() with get, set

    member val Distance : Nullable<decimal>  = Nullable() with get, set

    member val BatteryLevel : Nullable<double>  = Nullable() with get, set

    member val Petrol : Nullable<int>  = Nullable() with get, set

    member val Timestamp : Nullable<DateTime> = Nullable() with get, set

    member val HardwareId : string = null with get, set

    