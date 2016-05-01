using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;
using Newtonsoft.Json;

namespace Trackwane.Framework.Infrastructure.Web.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        /* Public */

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Log.Debug(String.Format("Received HTTP request <{0} with: ", GetUrl(actionContext.Request)));
            Log.Debug(Format(actionContext.Request));
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            Log.Debug(String.Format("Replying to HTTP request <{0} with: ", GetUrl(actionContext.Request)));
            Log.Debug(Format(actionContext.Response));
            base.OnActionExecuted(actionContext);
        }

        /* Private */

        private static string Format(HttpResponseMessage response)
        {
            return JsonConvert.SerializeObject(new
            {
                response.Content,
                response.StatusCode,
                response.Headers,
                response.ReasonPhrase
            }, Formatting.Indented);
        }

        private static string GetUrl(HttpRequestMessage request)
        {
            return request.Method + " " + request.RequestUri;
        }

        private static string Format(HttpRequestMessage request)
        {
            return JsonConvert.SerializeObject(new
            {
                Uri = request.Method + " " + request.RequestUri,
                request.Content,
                request.Headers,
            }, Formatting.Indented);
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof (LoggingFilter));
    }
}
