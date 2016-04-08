namespace Trackwane.AccessControl.Client

open Trackwane.Framework.Common.Interfaces
open Trackwane.Framework.Client
open Trackwane.AccessControl.Models

type AccessControlContext(baseUrl, config : IConfig) = 
    inherit ContextClient<AccessControlContext>(baseUrl, config)   
      
    let USER_RESOURCE_URL = "/organizations/{0}/users/{1}"
    let USER_COLLECTION_URL = "/organizations/{0}/users"
    let FIND_USER_BY_KEY_URL = "/users/{0}"
    let GET_ACCESS_TOKEN_URL = "/token?username={0}&password={1}"
    let CREATE_ROOT_USER_URL = "/root"
    let ORGANIZATION_RESOURCE_URL = "/organizations/{0}"
    let ORGANIZATION_COLLECTION_URL = "/organizations"
    let GRANT_VIEW_PERMISSION_URL =  ORGANIZATION_RESOURCE_URL + "/users/{1}}/view"
    let REVOKE_VIEW_PERMISSION_URL = ORGANIZATION_RESOURCE_URL + "/users/{{1}}/view"
    let GRANT_MANAGE_PERMISSION_URL = ORGANIZATION_RESOURCE_URL + "/users/{{1}}/manage"
    let REVOKE_MANAGE_PERMISSION_URL = ORGANIZATION_RESOURCE_URL + "/users/{{1}}/manage"
    let GRANT_ADMINISTRATE_PERMISSION_URL = ORGANIZATION_RESOURCE_URL + "/users/{{1}}/administrate"
    let REVOKE_ADMINISTRATE_PERMISSION_URL = ORGANIZATION_RESOURCE_URL + "/users/{{1}}/administrate"
    let COUNT_URL = ORGANIZATION_COLLECTION_URL + "/count"
    
    member this.CreateRootUser(model : RegisterUserModel) = 
        this.POST<UserDetails>(this.Expand(CREATE_ROOT_USER_URL), model);

    member this.RegisterUser(organizationKey : string, model: RegisterUserModel) =
        this.POST(this.Expand(USER_COLLECTION_URL, organizationKey), model)
 
    member this.GetAccessToken(username : string, password: string) : UserDetails =
        this.GET<UserDetails>(this.Expand(GET_ACCESS_TOKEN_URL, username, password))

    member this.ArchiveUser(organizationKey : string, userKey : string) =
        this.DELETE(this.Expand(USER_RESOURCE_URL, organizationKey, userKey))

    member this.UpdateUser(organizationKey : string, userKey : string, model : UpdateUserModel) =
        this.POST(this.Expand(USER_RESOURCE_URL, organizationKey, userKey), model)

    member this.FindUserByKey(userKey : string) : UserDetails =
        this.GET<UserDetails>(this.Expand(FIND_USER_BY_KEY_URL, userKey))
        
    member this.GrantAdministratePermission(organizationKey : string, userKey : string) =
        this.POST(this.Expand(GRANT_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey))

    member this.GrantManagePermission(organizationKey : string, userKey : string) =
        this.POST(this.Expand(GRANT_MANAGE_PERMISSION_URL, organizationKey, userKey))
            
    member this.GrantViewPermission(organizationKey : string, userKey : string) =
        this.POST(this.Expand(GRANT_VIEW_PERMISSION_URL, organizationKey, userKey))
            
    member this.RevokeAdministratePermission(organizationKey : string, userKey : string) =
        this.DELETE(this.Expand(REVOKE_ADMINISTRATE_PERMISSION_URL, organizationKey, userKey))

    member this.RevokeViewPermission(organizationKey : string, userKey : string) =
        this.DELETE(this.Expand(REVOKE_VIEW_PERMISSION_URL, organizationKey, userKey))

    member this.RevokeManagePermission(organizationKey : string, userKey : string) =
        this.DELETE(this.Expand(REVOKE_MANAGE_PERMISSION_URL, organizationKey, userKey))
            
    member this.ArchiveOrganization(organizationKey : string) =
        this.DELETE(this.Expand(ORGANIZATION_RESOURCE_URL, organizationKey))

    member this.UpdateOrganization(organizationId : string, model : UpdateOrganizationModel) =
        this.POST(this.Expand(ORGANIZATION_RESOURCE_URL, organizationId), model)

    member this.RegisterOrganization(model : RegisterOrganizationModel) =
        this.POST(this.Expand(ORGANIZATION_COLLECTION_URL), model)

    member this.FindByKey(organizationKey : string) : OrganizationDetails =
        this.GET<OrganizationDetails>(this.Expand(ORGANIZATION_RESOURCE_URL, organizationKey))

    member this.Count() : int =
        this.GET<int>(this.Expand(COUNT_URL))