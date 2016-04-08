namespace Trackwane.AccessControl.Events

open Trackwane.Framework.Common
open System

type OrganizationUpdatedState = {Name: string}
    
type OrganizationUpdated(organizationKey, previous, current) = 
    inherit DomainEvent()

    member val OrganizationKey: string = organizationKey 

    member val Current: OrganizationUpdatedState = previous 

    member val Previous: OrganizationUpdatedState = current

type OrganizationRegistered(organizationKey, name) = 
    inherit DomainEvent()

    member val OrganizationKey: string = organizationKey 

    member val Name: string = name

type OrganizationArchived(organizationKey) = 
    inherit DomainEvent()

    member val OrganizationKey: string = organizationKey
    
type AdministratePermissionGranted() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set

    member val OrganizationKey: string = null with get, set
    
type AdministratePermissionRevoked() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set

    member val OrganizationKey: string = null with get, set

type ManagePermissionGranted() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set

    member val OrganizationKey: string = null with get, set

type ManagePermissionRevoked() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set

    member val OrganizationKey: string = null with get, set

type ViewPermissionGranted() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set

    member val OrganizationKey: string = null with get, set

type ViewPermissionRevoked() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set

    member val OrganizationKey: string = null with get, set