using System.Web.Http;
using System.Web.Mvc;
using Trackwane.Data.Web;
using Trackwane.Framework.Web.DependencyResolution;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(DependencyResolution), "Start")]
[assembly: ApplicationShutdownMethod(typeof(DependencyResolution), "End")]

namespace Trackwane.Data.Web
{
    public static class DependencyResolution
    {
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        public static void End()
        {
            StructureMapDependencyScope.Dispose();
        }

        public static void Start()
        {
            var container = IoC.Initialize();
            container.AssertConfigurationIsValid();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope);
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
        }
    }
}