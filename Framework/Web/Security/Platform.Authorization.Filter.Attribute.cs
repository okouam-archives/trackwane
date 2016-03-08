using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Web.Security
{
    public abstract class PlatformAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (principal == null)
            {
                throw new Exception("No request principal found or not a claims principal");
            }

            var userClaims = new UserClaims(principal.Claims);
                
            if (!Check(userClaims, actionContext))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
        
        protected string GetOrganizationFromRouteData(HttpActionContext actionContext)
        {
            var routeData = actionContext.Request.GetRouteData();

            if (routeData.Values.ContainsKey("organizationKey"))
            {
                return routeData.Values["organizationKey"] as string;
            }

            var msg = "The current route does not provide any 'organizationKey' parameter. It contains the following parameters: " + string.Join("/", routeData.Values.Values);
            throw new Exception(msg);
        }

        public override bool AllowMultiple { get; } = false;

        /* Protected */

        protected abstract bool Check(UserClaims userClaims, HttpActionContext actionContext);
    }
}
