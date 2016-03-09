using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Trackwane.Data.Engine;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
using Web;

namespace Trackwane.Data.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var serviceLocationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new DocumentStoreConfig()));

            var serviceLocator = new ServiceLocator<Engine.Registry>(serviceLocationFactory);

            var host = new EngineHost<Engine.Registry>(serviceLocator, new EngineHostConfig
            {
                Listeners = typeof(_Data_Engine_Assembly_).Assembly.GetListeners(),
                Events = Enumerable.Empty<Type>()
            });

            host.Start();
        }
    }
}
