using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Trackwane.AccessControl.Events;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
using Trackwane.Management.Engine;
using Trackwane.Management.Events;

namespace Trackwane.Management.Web
{
    public class MvcApplication : System.Web.HttpApplication
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
                Listeners = typeof(_Management_Engine_Assembly_).Assembly.GetListeners(), 
                Events = typeof(_Access_Control_Events_Assembly_).Assembly.GetDomainEvents().Union(typeof(_Management_Events_Assembly_).Assembly.GetDomainEvents()),
            });

            host.Start();
        }
    }
}
