﻿using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Events;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Events;
using Trackwane.Management.Queries.Drivers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Drivers
{
    [TestFixture]
    internal class UpdateDriverTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string DRIVER_ID;
        private string USER_ID;

        [SetUp]
        public void SetUp()
        {
            DRIVER_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Publishes_Driver_Updated_Event()
        {
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID, "John Smith");
            _Update_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID, "Karen Smith");

            WasPosted<DriverUpdated>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID, "John Smith");
            _Update_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID, "Karen Smith");

            var driver = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(DRIVER_ID);
            driver.ShouldNotBeNull();
            driver.Name.ShouldBe("Karen Smith");
        }
    }
}
