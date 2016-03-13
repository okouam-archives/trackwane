using System;
using Serilog;
using Topshelf;
using Trackwane.AccessControl.Engine;
using Trackwane.AccessControl.Events;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;

namespace Trackwane.AccessControl.Standalone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Seq("http://localhost:5341")
               .CreateLogger();

            HostFactory.Run(x =>
            {
                var serviceLocationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new DocumentStoreConfig()));

                var serviceLocator = new ServiceLocator<Engine.Registry>(serviceLocationFactory);

                x.Service<EngineHost<Engine.Registry>> (s =>
                {
                    s.WhenStarted(y => y.Start());
                    s.WhenStopped(y => y.Stop());
                    s.ConstructUsing(y => new EngineHost<Engine.Registry>(serviceLocator, new EngineHostConfig
                    {
                        Events = typeof(_Access_Control_Events_Assembly_).Assembly.GetDomainEvents(),
                        Handlers = typeof(_Access_Control_Engine_Assembly_).Assembly.GetHandlers(),
                        Listeners = typeof(_Access_Control_Engine_Assembly_).Assembly.GetListeners(),
                        Commands = typeof(_Access_Control_Engine_Assembly_).Assembly.GetCommands(),
                        ListenUri = new Uri("http://localhost:37483")
                    }));
                });

                x.RunAsLocalSystem();
                x.SetDisplayName("Trackwane.Access.Control");
                x.SetServiceName("Trackwane.Access.Control");
            });
        }
    }
}

