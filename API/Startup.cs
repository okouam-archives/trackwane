using System.Web.Http;
using FluentValidation.WebApi;
using Marten;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using StructureMap;
using Swashbuckle.Application;
using Trackwane.API;
using Trackwane.Framework.Infrastructure.Web;
using Trackwane.Framework.Infrastructure.Web.DependencyResolution;
using Trackwane.Framework.Infrastructure.Web.Filters;
using Trackwane.Framework.Interfaces;

[assembly: OwinStartup(typeof(Startup))]

namespace Trackwane.API
{
    public class Startup
    {
        public static IContainer Container { get; set; }

        public void Configuration(IAppBuilder app)
        {
            Container = new Container(x =>
            {
                x.AddRegistry<AccessControl.Engine.Registry>();
                x.AddRegistry<Data.Engine.Registry>();
                x.AddRegistry<Management.Engine.Registry>();
                x.AddRegistry<Framework.Infrastructure.Registry>();
                x.For<IDocumentStore>().Use(DocumentStore.For("Server=127.0.0.1;Port=5432;Database=trackwane;User Id=postgres;Password = com99123; "));
            });

            Container.GetInstance<IExecutionEngine>().Start();

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            var config = new HttpConfiguration();
            ApplyConfiguration(Container, config);
            app.UseWebApi(config);
        }
        
        private static void ApplyConfiguration(IContainer container, HttpConfiguration configuration)
        {
            configuration.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Trackwane API");
                c.UseFullTypeNameInSchemaIds();
            }).EnableSwaggerUi();

            FluentValidationModelValidatorProvider.Configure(configuration, config =>
            {
                config.ValidatorFactory = new WebApiValidatorFactory(configuration);
            });

            ResolveDependencies(configuration, container);

            RegisterRoutes(configuration);
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new BusinessRuleExceptionFilter());
            config.Filters.Add(new ValidationExceptionFilter());
            config.Filters.Add(new NotFoundExceptionFilter());
        }

        private static void ResolveDependencies(HttpConfiguration configuration, IContainer container)
        {
            container.AssertConfigurationIsValid();
            configuration.DependencyResolver = new WebApiDependencyResolver(container, false);
        }
    }
}
