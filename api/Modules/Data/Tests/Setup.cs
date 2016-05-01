using System.Linq;
using log4net.Config;
using NUnit.Framework;
using Trackwane.AccessControl.Engine;
using Trackwane.Contracts;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Interfaces;
using Registry = Trackwane.AccessControl.Engine.Registry;

namespace Trackwane.Tests
{
    [SetUpFixture]
// ReSharper disable once CheckNamespace
    public class Setup
    {
        public static IEngineHost EngineHost { get; set; }
        
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            XmlConfigurator.Configure();

            var engine = typeof (_Engine_).Assembly;

            var module = new ModuleConfig(engine);

            const string server = "localhost";

            EngineHost = new EngineHost<Registry>(server, module, engine, typeof(_Contracts_).Assembly.GetDomainEvents().ToArray());

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            EngineHost.Stop();
        }
    }
}


