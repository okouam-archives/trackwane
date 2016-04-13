using System;
using System.Reflection;
using log4net;
using log4net.Config;
using Trackwane.AccessControl.Engine;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;

namespace Trackwane.AccessControl.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            log.Info("Starting Trackwane.AccessControl");

            var assembly = typeof(_Access_Control_Engine_Assembly_).Assembly;

            var moduleConfig = new ModuleConfig(assembly);
            
            log.Info("Running the service for module <" + moduleConfig.ModuleName + ">");

            log.Info("Using the etcd instance located at <" + moduleConfig.Etcd + ">");

            var config = new EngineHostConfig(assembly.GetCommands(), assembly.GetDomainEvents(), assembly.GetHandlers(), assembly.GetListeners(), new Uri(moduleConfig.Get("uri")));

            var locator = new ServiceLocator<Engine.Registry>(new ServiceLocationFactory(new DocumentStoreBuilder(moduleConfig)));

            var host = new EngineHost<Engine.Registry>(locator, config);
            
            host.Start();

            log.Info("Listening for connections at <" + moduleConfig.Get("uri") + ">");

            Console.ReadLine();

            log.Info("Stopping Trackwane.AccessControl");

            host.Stop();
        }

        static readonly ILog log = LogManager.GetLogger("Trackwane.AccessControl");
    }
}
