using System;
using System.Linq;
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
                var hasAppKey = Request.Headers.Contains(Constants.HTTP_TRACKWANE_APPLICATION_KEY);

                if (!hasAppKey)
                {
                    throw new Exception($"The HTTP Header {Constants.HTTP_TRACKWANE_APPLICATION_KEY} cannot be found");
                }

                return Request.Headers.GetValues(Constants.HTTP_TRACKWANE_APPLICATION_KEY).First(); 
            }
        }

        protected UserClaims CurrentClaims
        {
            get { return new UserClaims((User as ClaimsPrincipal).Claims); }
        }
    }
}
