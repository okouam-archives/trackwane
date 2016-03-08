using paramore.brighter.commandprocessor;
using StructureMap;
using StructureMap.Graph;
using Trackwane.Data.Dependencies;
using Trackwane.Framework.Web.DependencyResolution;
using CommandProcessorBuilder = Trackwane.Framework.Infrastructure.CommandProcessorBuilder;

namespace Trackwane.Data.Web {
    public static class IoC {
        public static IContainer Initialize()
        {
            var container = new Container(c =>
            {
                c.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
            });

            ConfigureCommandProcessor(container);

            return container;
        }

        private static void ConfigureCommandProcessor(IContainer container)
        {
            container.Configure(x =>
            {
                var subscribers = ServiceLocator.GetSubscribers();
                var dataContainer = ServiceLocator.GetContainer();
                x.For<IAmACommandProcessor>().Use("IAmACommandProcessor", c => CommandProcessorBuilder.Build(subscribers, dataContainer));
            });
        }
    }
}