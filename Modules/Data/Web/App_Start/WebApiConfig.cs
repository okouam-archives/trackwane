using System.Web.Http;

namespace Trackwane.Data.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.MapHttpAttributeRoutes();
        }
    }
}
