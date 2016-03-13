using System;
using Microsoft.Owin.Hosting;
using Serilog;
using Serilog.Core.Enrichers;
using Topshelf;
using Trackwane.Framework.Common.Configuration;

namespace Trackwane.Simulator.Standalone
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
                x.Service<Application>(s =>
                {
                    s.ConstructUsing(name => new Application());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDisplayName(moduleConfig.DisplayName);
                x.SetServiceName(moduleConfig.ServiceName);
            });
        }

        private class Application
        {
            private IDisposable _host;

            public void Start()
            {
                var moduleConfig = new ModuleConfig();

                _host = WebApp.Start<Startup>(moduleConfig.Uri);

                Console.WriteLine();
                Console.WriteLine("Hangfire Server started.");
                Console.WriteLine("Dashboard is available at {0}/hangfire", moduleConfig.Uri);
                Console.WriteLine();
            }

            public void Stop()
            {
                _host.Dispose();
            }
        }
    }
}
