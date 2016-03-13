using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Serilog;

namespace Trackwane.Framework.Web.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        /* Public */

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Log.Debug($"Received HTTP request <{GetUrl(actionContext.Request)} with: ");
            Log.Debug(Format(actionContext.Request));
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            Log.Debug($"Replying to HTTP request <{GetUrl(actionContext.Request)} with: ");
            Log.Debug(Format(actionContext.Response));
            base.OnActionExecuted(actionContext);
        }

        /* Private */

        private static string Format(HttpResponseMessage response) =>
            JsonConvert.SerializeObject(new
            {
                response.Content,
                response.StatusCode,
                response.Headers,
                response.ReasonPhrase
            }, Formatting.Indented);
        
        private static string GetUrl(HttpRequestMessage request) =>
            request.Method + " " + request.RequestUri;
        
        private static string Format(HttpRequestMessage request) =>
            JsonConvert.SerializeObject(new
            {
                Uri = request.Method + " " + request.RequestUri,
                request.Content,
                request.Headers,
            }, Formatting.Indented);
    }
}
