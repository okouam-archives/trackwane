using System.Web.Http;
using Owin;
using StructureMap;
using Trackwane.Framework.Infrastructure.Web.DependencyResolution;
using Trackwane.Framework.Infrastructure.Web.Filters;

namespace Trackwane.Framework.Infrastructure
{
    public class Startup
    {
        public static IContainer Container { get; set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            ApplyConfiguration(Container, config);
            appBuilder.UseWebApi(config);
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