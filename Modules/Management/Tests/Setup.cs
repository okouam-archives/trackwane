using System;
using System.Linq;
using NUnit.Framework;
using Trackwane.AccessControl.Contracts;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
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
        public static IEngineHost EngineHost { get; set; }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var serviceLocationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new DocumentStoreConfig()));

            EngineHost = new EngineHost<Registry>(new ServiceLocator<Registry>(serviceLocationFactory), new EngineHostConfig
            {
                ListenUri = new Uri("http://localhost:8343"),

                Events = typeof(_Access_Control_Contracts_Assembly_).Assembly.GetDomainEvents()
                            .Union(typeof(_Management_Contracts_Assembly_).Assembly.GetDomainEvents()),

                Handlers = typeof(_Management_Engine_Assembly_).Assembly.GetHandlers(),

                Commands = typeof(_Management_Engine_Assembly_).Assembly.GetCommands(),

                Listeners = typeof(_Management_Engine_Assembly_).Assembly.GetListeners()
            });

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests() => EngineHost.Stop();
    }
}

