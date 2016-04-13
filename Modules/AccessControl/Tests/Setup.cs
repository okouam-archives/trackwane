using System;
using System.Linq;
using NUnit.Framework;
using Trackwane.AccessControl.Contracts;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.AccessControl.Engine;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.AccessControl.Engine.Processors.Handlers.Users;
using Trackwane.AccessControl.Engine.Processors.Listeners;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
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

            EngineHost = new EngineHost<Registry>(module, engine, typeof(_Access_Control_Contracts_Assembly_).Assembly.GetDomainEvents().ToArray());

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            EngineHost.Stop();
        }
    }
}


