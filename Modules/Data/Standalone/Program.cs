using System;
using System.Linq;
using Serilog;
using Serilog.Core.Enrichers;
using Topshelf;
using Trackwane.Data.Engine;
using Trackwane.Data.Events;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
using Trackwane.Management.Events;

namespace Trackwane.Data.Standalone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var moduleConfig = new ModuleConfig();

            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new PropertyEnricher("Module", moduleConfig.Name))
                .WriteTo.Seq(new LoggingConfig().Uri)
                .CreateLogger();

            HostFactory.Run(x =>
            {
                var serviceLocationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new DocumentStoreConfig()));

                var serviceLocator = new ServiceLocator<Engine.Registry>(serviceLocationFactory);

                x.Service<EngineHost<Engine.Registry>>(s =>
                {
                    s.WhenStarted(y => y.Start());
                    s.WhenStopped(y => y.Stop());
                    s.ConstructUsing(y => new EngineHost<Engine.Registry>(serviceLocator, new EngineHostConfig
                    {
                        Events = typeof(_Management_Events_Assembly_).Assembly.GetDomainEvents().Union(typeof(_Data_Events_Assembly_).Assembly.GetDomainEvents()),
                        Handlers = typeof(_Data_Engine_Assembly_).Assembly.GetHandlers(),
                        Listeners = typeof(_Data_Engine_Assembly_).Assembly.GetListeners(),
                        Commands = typeof(_Data_Engine_Assembly_).Assembly.GetCommands(),
                        ListenUri = new Uri(new ModuleConfig().Uri)
                    }));
                });

                x.RunAsLocalSystem();
                x.SetDisplayName(moduleConfig.DisplayName);
                x.SetServiceName(moduleConfig.ServiceName);
            });
        }
    }
}
