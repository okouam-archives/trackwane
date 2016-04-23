using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Trackwane.Data.Shared.Client;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Data.Tests.Helpers
{
    internal partial class Scenario
    {
        public Scenario()
        {
            Posted = new List<IRequest>();
        }

        protected string ApplicationKey { get; private set; }

        protected IList<IRequest> Posted { get; set; }

        protected IEngineHost EngineHost
        {
            get { return Setup.EngineHost; }
        }

        protected static DataContext Client { get; set; }

        [SetUp]
        public void BeforeEachTest()
        {
            var server = "localhost";
            var platformConfig = new PlatformConfig();
            var apiPort = Setup.EngineHost.Configuration.Get("api-port");
            var metricsPort = Setup.EngineHost.Configuration.Get("metrics-port");
            var secretKey = platformConfig.SecretKey;
            var protocol = Setup.EngineHost.Configuration.Get("protocol");

            Client = new DataContext(server, protocol, apiPort, secretKey, metricsPort, ApplicationKey);

            EngineHost.ExecutionEngine.MessagePosted += (o, request) => Posted.Add(request); ;
        }

        protected bool WasPosted<T>()
        {
            return Posted.Any(x => x.GetType() == typeof(T));
        }
    }
}
