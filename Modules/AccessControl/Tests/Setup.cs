using System.Linq;
using NUnit.Framework;
using Trackwane.AccessControl.Contracts;
using Trackwane.AccessControl.Engine;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Interfaces;
using Registry = Trackwane.AccessControl.Engine.Registry;

namespace Trackwane.AccessControl.Tests
{
    [SetUpFixture]
// ReSharper disable once CheckNamespace
    public class Setup
    {
        public static IEngineHost EngineHost { get; set; }
        
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var engine = typeof (_Access_Control_Engine_Assembly_).Assembly;

            var module = new ModuleConfig(engine);

            var server = "localhost";

            EngineHost = new EngineHost<Registry>(server, module, engine, typeof(_Access_Control_Contracts_Assembly_).Assembly.GetDomainEvents().ToArray());

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            EngineHost.Stop();
        }
    }
}


