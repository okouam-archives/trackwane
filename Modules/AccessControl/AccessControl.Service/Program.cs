using System;
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
            var config = new EngineHostConfig(null, null, null, null, null);

            var locator = new ServiceLocator<Engine.Registry>(new ServiceLocationFactory(new DocumentStoreBuilder(new Config())));

            var host = new EngineHost<Engine.Registry>(locator, config);

            host.Start();

            Console.ReadLine();

            host.Stop();
        }
    }
}
