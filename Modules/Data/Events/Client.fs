namespace Trackwane.Data.Cient

open Trackwane.Framework.Common.Interfaces
open Trackwane.Framework.Client
open Trackwane.Data.Contracts.Models

type DataContext(baseUrl, config : IPlatformConfig) = 
    inherit ContextClient<DataContext>(baseUrl, config)   

    member this.SaveSensorReading(model : SaveSensorReadingModel) = 
        this.POST(this.Expand("data"), model)