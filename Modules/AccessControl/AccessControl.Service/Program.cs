using System;
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

            var assembly = typeof (_Access_Control_Engine_Assembly_).Assembly;

            var config = new EngineHostConfig(assembly.GetCommands(), assembly.GetDomainEvents(), assembly.GetHandlers(), assembly.GetListeners(), new Uri("http://localhost:8373"));

            var locator = new ServiceLocator<Engine.Registry>(new ServiceLocationFactory(new DocumentStoreBuilder(new Config())));

            var host = new EngineHost<Engine.Registry>(locator, config);

            log.Info("Starting Trackwane.AccessControl");

            host.Start();

            log.Info("Trackwane.AccessControl is running and waiting for connections");

            Console.ReadLine();

            log.Info("Stopping Trackwane.AccessControl");

            host.Stop();
        }

        static readonly ILog log = LogManager.GetLogger("Trackwane.AccessControl");
    }
}
