namespace Trackwane.AccessControl.Events

open Trackwane.Framework.Common
open System

type UserUpdatedState = {Email: string; DisplayName: string}

type UserUpdated(userKey, previous, current) = 
    inherit DomainEvent()

    member val UserKey: string = userKey 

    member val Previous: UserUpdatedState = previous 

    member val Current: UserUpdatedState = current

type UserRegistered() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set 

    member val ParentOrganizationKey: string = null with get, set 

    member val DisplayName: string = null with get, set 

    member val Role: string = null with get, set 

    member val Email: string = null with get, set 

    member val Password: string = null with get, set 

type UserArchived() = 
    inherit DomainEvent()

    member val UserKey: string = null with get, set