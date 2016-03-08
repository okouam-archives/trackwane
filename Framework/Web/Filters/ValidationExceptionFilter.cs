using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace Trackwane.Framework.Web.Filters
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        /* Public */

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            if (exception is FluentValidation.ValidationException)
            {
                var responseContent = JsonConvert.SerializeObject(new
                {
                    message = exception.Message,
                    type = exception.GetType().Name
                });

                actionExecutedContext.Response = new HttpResponseMessage
                {
                    Content = new StringContent(responseContent, Encoding.UTF8, "text/plain"),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }

            base.OnException(actionExecutedContext);
        }
    }
}
