using System;
using System.Collections.Generic;
using System.Linq;
using Owin;
using paramore.brighter.commandprocessor;
using StructureMap;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.AccessControl.Engine.Processors.Handlers.Users;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web;
using Registry = Trackwane.AccessControl.Engine.Registry;

namespace Trackwane.AccessControl.Service
{
    public static class Startup
    {
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            var locator = new ServiceLocator<Registry>(new ServiceLocationFactory(new DocumentStoreBuilder(new Config())));
            var container = WireUpDependencies(locator, typeof(ArchiveUser).Assembly.GetCommands().ToList(), typeof(ArchiveUserHandler).Assembly.GetHandlers().ToList());
            var config = WebApiBootstrapper.CreateServer(container);
            appBuilder.UseWebApi(config);
        }

        private static IContainer WireUpDependencies(IServiceLocator<Registry> locator, IReadOnlyCollection<Type> commands, IEnumerable<Type> handlers)
        {
            var container = locator.GetContainer();
            var mapperFactory = new MapperFactory(container).WithCommands(commands);

            container.Configure(x =>
            {
                var subscribers = locator.GetSubscribers(null, handlers, null, commands);
                var processor = CommandProcessorFactory.Build(subscribers, container, mapperFactory);
                x.For<IAmACommandProcessor>().Singleton().Use(processor);
                x.For<IExecutionEngine>().Use(new ExecutionEngine(container, processor)).Singleton();
            });

            container.AssertConfigurationIsValid();

            return container;
        }
    }
}
