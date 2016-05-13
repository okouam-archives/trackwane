using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Trackwane.Framework.Common.Exceptions;

namespace Trackwane.Framework.Infrastructure.Web.Filters
{
    public class NotFoundExceptionFilter : ExceptionFilterAttribute
    {
        /* Public */

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            if (exception is NotFoundException)
            {
                var responseContent = JsonConvert.SerializeObject(new
                {
                    message = exception.Message
                });

                actionExecutedContext.Response = new HttpResponseMessage
                {
                    Content = new StringContent(responseContent, Encoding.UTF8, "text/plain"),
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            base.OnException(actionExecutedContext);
        }
    }
}
