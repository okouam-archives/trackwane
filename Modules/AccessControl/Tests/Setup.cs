using System;
using NUnit.Framework;
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
            var serviceLocationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new ModuleConfig(typeof(_Access_Control_Engine_Assembly_).Assembly)));

            EngineHost = new EngineHost<Registry>(new ServiceLocator<Registry>(serviceLocationFactory), new EngineHostConfig
            {
                ListenUri = new Uri("http://localhost:8343"),
                Events = typeof(UserArchived).Assembly.GetDomainEvents(),
                Handlers = typeof(ArchiveUserHandler).Assembly.GetHandlers(),
                Commands = typeof(ArchiveUser).Assembly.GetCommands(),
                Listeners = typeof(UserArchivedListener).Assembly.GetListeners()
            });

            EngineHost.Start();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            EngineHost.Stop();
        }
    }
}


