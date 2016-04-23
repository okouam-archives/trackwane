using System;
using HashidsNet;
using NUnit.Framework;
using Trackwane.AccessControl.Contracts;
using Trackwane.Framework.Common.Configuration;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected string ApplicationKey { get; private set; }

        protected static AccessControlClient Client { get; set; }
        
        [SetUp]
        public void BeforeEachTest()
        {
            ApplicationKey = GenerateKey();
            Client = SetupClient(ApplicationKey);
            Register_Application.With(new PlatformConfig().SecretKey, GenerateKey(), "application_creator@nowhere.com", "xxx", "APPLICATION CREATOR");
        }

        protected static AccessControlClient SetupClient(string appKey = null)
        {
            var server = "localhost";
            var platformConfig = new PlatformConfig();
            var apiPort = Setup.EngineHost.Configuration.ApiConfig.Port;
            var metricsPort = Setup.EngineHost.Configuration.MetricConfig.Port;
            var secretKey = platformConfig.SecretKey;
            var protocol = Setup.EngineHost.Configuration.Get("protocol");

            return new AccessControlClient(server, protocol, apiPort, secretKey, metricsPort, appKey);
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
