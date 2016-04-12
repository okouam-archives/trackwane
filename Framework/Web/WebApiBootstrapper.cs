using System.Web.Http;
using System.Web.Http.SelfHost;
using StructureMap;
using Swashbuckle.Application;
using Trackwane.Framework.Web.DependencyResolution;
using Trackwane.Framework.Web.Filters;

namespace Trackwane.Framework.Web
{
    public class WebApiBootstrapper
    {
        /* Public */

        public static HttpSelfHostServer CreateServer(IContainer container, string url)
        {
            var configuration = new HttpSelfHostConfiguration(url);
            ApplyConfiguration(container, configuration);
            return new HttpSelfHostServer(configuration);
        }

        public static HttpConfiguration CreateServer(IContainer container)
        {
            var configuration = new HttpConfiguration();
            ApplyConfiguration(container, configuration);
            return configuration;
        }

        private static void ApplyConfiguration(IContainer container, HttpConfiguration configuration)
        {
            configuration.EnableSwagger(c =>
            {
                //c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
                c.SingleApiVersion("v1", "Trackwane API");
                c.UseFullTypeNameInSchemaIds();
            }).EnableSwaggerUi();

            ResolveDependencies(configuration, container);
            RegisterRoutes(configuration);
        }

        /* Private */

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
