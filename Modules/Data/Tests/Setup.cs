using System;
using NUnit.Framework;
using Trackwane.Data.Contracts;
using Trackwane.Data.Engine;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
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
            var serviceLocationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new DocumentStoreConfig()));

            EngineHost = new EngineHost<Registry>(new ServiceLocator<Registry>(serviceLocationFactory), new EngineHostConfig
            {
                ListenUri = new Uri("http://localhost:8343"),
                Events = typeof(_Data_Contracts_Assembly_).Assembly.GetDomainEvents(),
                Handlers = typeof(_Data_Engine_Assembly_).Assembly.GetHandlers(),
                Commands = typeof(_Data_Engine_Assembly_).Assembly.GetCommands(),
                Listeners = typeof(_Data_Engine_Assembly_).Assembly.GetListeners()
            });

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests() => EngineHost.Stop();
    }
}

