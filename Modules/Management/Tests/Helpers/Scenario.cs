using System;
using HashidsNet;
using NUnit.Framework;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Management.Contracts;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected string ApplicationKey { get; private set; }

        protected static ManagementContext Client { get; set; }

        [SetUp]
        public void BeforeEachTest()
        {
            ApplicationKey = GenerateKey();
            Client = SetupClient(ApplicationKey);
        }

        protected static ManagementContext SetupClient(string appKey = null)
        {
            var server = "localhost";
            var platformConfig = new PlatformConfig();
            var apiPort = Setup.EngineHost.Configuration.Get("api-port");
            var metricsPort = Setup.EngineHost.Configuration.Get("metrics-port");
            var secretKey = platformConfig.SecretKey;
            var protocol = Setup.EngineHost.Configuration.Get("protocol");

            return new ManagementContext(server, protocol, apiPort, secretKey, metricsPort, appKey);
        }

        protected bool WasPosted<T>()
        {
            return Client.Metrics.Published<T>() > 0;
        }

        protected static string GenerateKey()
        {
            return new Hashids().EncodeLong(DateTime.Now.Ticks) + SEQUENCE++;
        }

        private static long SEQUENCE;
    }
}
