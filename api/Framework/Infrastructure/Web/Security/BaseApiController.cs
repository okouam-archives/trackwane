using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Http;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Infrastructure.Web.Security
{
    public class BaseApiController : ApiController
    {
        protected string AppKeyFromHeader 
        {
            get
            {
                return Request.Headers.GetValues(Constants.HTTP_TRACKWANE_APPLICATION_KEY).First(); 
            }
        }

        protected UserClaims CurrentClaims
        {
            get { return new UserClaims((User as ClaimsPrincipal).Claims); }
        }
    }
}
