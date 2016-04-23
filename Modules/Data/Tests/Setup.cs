using System.Linq;
using NUnit.Framework;
using Trackwane.Data.Engine;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Interfaces;
using Registry = Trackwane.Data.Engine.Registry;

namespace Trackwane.Data.Tests
{
    [SetUpFixture]
    // ReSharper disable once CheckNamespace
    public class Setup
    {
        public static IEngineHost EngineHost { get; set; }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var engine = typeof (_Data_Engine_Assembly_).Assembly;

            var config = new ModuleConfig(engine);

            EngineHost = new EngineHost<Registry>("localhost", config, engine, engine.GetDomainEvents().ToArray());

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            EngineHost.Stop();
        }
    }
}

