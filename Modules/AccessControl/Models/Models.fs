namespace Trackwane.AccessControl.Models

open System
open System.Collections.Generic

type RegisterUserModel() = 
    member val Password : string = null with get, set
    member val Email : string = null with get, set
    member val DisplayName : string = null with get, set
    member val UserKey : string = null with get, set

type UpdateUserModel() = 
    member val Password : string = null with get, set
    member val Email : string = null with get, set
    member val DisplayName : string = null with get, set

type UserSummary() = 
    member val Id : string = null with get, set
    member val Email : string = null with get, set
    member val DisplayName : string = null with get, set

type UserDetails() = 
    member val Key : string = null with get, set
    member val IsArchived : bool = false with get, set
    member val DisplayName : string = null with get, set
    member val Email : string = null with get, set
    member val Role : string = null with get, set
    member val View : IList<Tuple<string, string>> = null with get, set
    member val Manage : IList<Tuple<string, string>> = null with get, set
    member val Administrate : IList<Tuple<string, string>> = null with get, set
    member val ParentOrganizationKey : string = null with get, set

 type OrganizationDetails() = 
    member val Key : string = null with get, set
    member val Name : string = null with get, set
    member val IsArchived : bool = false with get, set
    member val Viewers : IList<UserSummary> = null with get, set
    member val Managers : IList<UserSummary> = null with get, set
    member val Administrators : IList<UserSummary> = null with get, set

type UpdateOrganizationModel() = 
    member val Name : string = null with get, set

type RegisterOrganizationModel() = 
    member val Name : string = null with get, set
    member val OrganizationKey : string = null with get, set