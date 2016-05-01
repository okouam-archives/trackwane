using System.Web.Http.Controllers;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Infrastructure.Web.Security
{
    public class ManagersAttribute : PlatformAuthorizationFilterAttribute
    {
        protected override bool Check(UserClaims userClaims, HttpActionContext actionContext)
        {
            if (userClaims.IsSystemManager) return true;

            var organizationKey = GetOrganizationFromRouteData(actionContext);

            return userClaims.CanManage(organizationKey);
        }
    }
}