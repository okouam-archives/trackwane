using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using StructureMap;
using Trackwane.Framework.Infrastructure.Web.DependencyResolution;
using Trackwane.Framework.Infrastructure.Web.Filters;

[assembly: OwinStartup(typeof(API.Startup))]

namespace API
{
    public partial class Startup
    {
        public static IContainer Container { get; set; }


        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            var config = new HttpConfiguration();
            ApplyConfiguration(Container, config);
            app.UseWebApi(config);
        }
        
        private static void ApplyConfiguration(IContainer container, HttpConfiguration configuration)
        {
            ResolveDependencies(configuration, container);
            RegisterRoutes(configuration);
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new BusinessRuleExceptionFilter());
            config.Filters.Add(new ValidationExceptionFilter());
        }

        private static void ResolveDependencies(HttpConfiguration configuration, IContainer container)
        {
            container.AssertConfigurationIsValid();
            configuration.DependencyResolver = new WebApiDependencyResolver(container, false);
        }
    }
}
