using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using Trackwane.Framework.Common;
using Trackwane.Framework.Web.Security;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class AdministratorsOrUserAttribute : PlatformAuthorizationFilterAttribute
    {
        protected override bool Check(UserClaims userClaims, HttpActionContext actionContext)
        {
            var organizationKey = GetOrganizationFromRouteData(actionContext);
            var userKey = GetUserFromRouteData(actionContext);
            return userClaims.CanAdministrate(organizationKey) || userClaims.UserId == userKey;
        }

        protected string GetUserFromRouteData(HttpActionContext actionContext)
        {
            var routeData = actionContext.Request.GetRouteData();

            if (routeData.Values.ContainsKey("userKey"))
            {
                return routeData.Values["userKey"] as string;
            }

            var msg = "The current route does not provide any 'userKey' parameter. It contains the following parameters: " + string.Join("/", routeData.Values.Values);
            throw new Exception(msg);
        }
    }
}