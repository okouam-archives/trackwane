using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Engine.Queries.Drivers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Drivers
{
    [TestFixture]
    internal class ArchiveDriverTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string DRIVER_ID;
        private string USER_ID;

        [SetUp]
        public void SetUp()
        {
            DRIVER_ID = "driver-" + Guid.NewGuid();
            ORGANIZATION_KEY = "organization-" + Guid.NewGuid();
            USER_ID = "user-" + Guid.NewGuid();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void On_Success_Publishes_Driver_Archived_Event()
        {
            _Register_Driver.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, DRIVER_ID);
            _ArchiveDriver.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, DRIVER_ID);

            WasPosted<DriverArchived>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Driver.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, DRIVER_ID);
            _ArchiveDriver.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, DRIVER_ID);

            var driver = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(DRIVER_ID);
            driver.IsArchived.ShouldBeTrue();
        }
    }
}
