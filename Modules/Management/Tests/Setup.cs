using System.Linq;
using NUnit.Framework;
using Trackwane.AccessControl.Contracts;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts;
using Trackwane.Management.Engine;
using Registry = Trackwane.Management.Engine.Registry;

namespace Trackwane.Management.Tests
{
    [SetUpFixture]
    // ReSharper disable once CheckNamespace
    public class Setup
    {
        public static string ApplicationKey { get; set; }

        public static IEngineHost EngineHost { get; set; }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var engine = typeof (_Management_Engine_Assembly_).Assembly;

            var events = typeof (_Access_Control_Contracts_Assembly_).Assembly.GetDomainEvents()
                .Union(typeof (_Management_Contracts_Assembly_).Assembly.GetDomainEvents()).ToArray();

            var config = new ModuleConfig(engine);

            EngineHost = new EngineHost<Registry>("localhost", config, engine,  events);

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            EngineHost.Stop();
        }
    }
}

