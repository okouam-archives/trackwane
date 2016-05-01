using System;
using System.Linq;
using log4net;
using log4net.Config;
using Mono.Unix;
using Mono.Unix.Native;
using Trackwane.AccessControl.Engine;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;

namespace Trackwane.AccessControl.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            log.Info("Starting Trackwane.AccessControl");

            var assembly = typeof(_Engine_).Assembly;

            var moduleConfig = new ModuleConfig(assembly);
            
            log.Info("Running the service for module <" + moduleConfig.ModuleName + ">");

            log.Info("Using the etcd instance located at <" + moduleConfig.Etcd + ">");

            var host = new EngineHost<Engine.Registry>("localhost", moduleConfig, assembly, assembly.GetDomainEvents().ToArray());

            host.Start();

            log.Info("Listening for connections at <" + moduleConfig.Get("uri") + ">");


            if (IsRunningOnMono())
            {
                var terminationSignals = GetUnixTerminationSignals();
                UnixSignal.WaitAny(terminationSignals);
            }
            else
            {
                Console.ReadLine();
            }

            log.Info("Stopping Trackwane.AccessControl");

            host.Stop();
        }

        private static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private static UnixSignal[] GetUnixTerminationSignals()
        {
            return new[]
            {
                new UnixSignal(Signum.SIGINT),
                new UnixSignal(Signum.SIGTERM),
                new UnixSignal(Signum.SIGQUIT),
                new UnixSignal(Signum.SIGHUP)
            };
        }

        static readonly ILog log = LogManager.GetLogger("Trackwane.AccessControl");
    }
}
