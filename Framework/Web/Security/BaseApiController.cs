using System.Security.Claims;
using System.Web.Http;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Web.Security
{
    public class BaseApiController : ApiController
    {
        protected UserClaims CurrentClaims => new UserClaims((User as ClaimsPrincipal).Claims);
    }
}
