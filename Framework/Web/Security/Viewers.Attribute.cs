using System.Web.Http.Controllers;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Web.Security
{
    public class Viewers : PlatformAuthorizationFilterAttribute
    {
        protected override bool Check(UserClaims userClaims, HttpActionContext actionContext)
        {
            if (userClaims.IsSystemManager) return true;

            var organizationKey = GetOrganizationFromRouteData(actionContext);

            return userClaims.CanView(organizationKey);
        }
    }
}