using System.Web.Http.Controllers;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Infrastructure.Web.Security
{
    public class SystemManagersAttribute : PlatformAuthorizationFilterAttribute
    {
        protected override bool Check(UserClaims userClaims, HttpActionContext actionContext)
        {
            return userClaims.IsSystemManager;
        }
    }
}