using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Trackwane.Data.Shared.Client;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Tests.Helpers
{
    internal partial class Scenario
    {
        protected string ApplicationKey { get; private set; }

        protected IEngineHost EngineHost
        {
            get { return null; }
        }

        protected static DataContext Client { get; set; }

        [SetUp]
        public void BeforeEachTest()
        {
            var server = "localhost";
            var platformConfig = new PlatformConfig();
           // var apiPort = Setup.EngineHost.Configuration.Get("api-port");
           // var metricsPort = Setup.EngineHost.Configuration.Get("metrics-port");
            //var secretKey = platformConfig.SecretKey;
            //var protocol = Setup.EngineHost.Configuration.Get("protocol");

            //Client = new DataContext(server, protocol, apiPort, secretKey, metricsPort, ApplicationKey);

        }
    }
}
