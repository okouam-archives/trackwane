using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Drivers;
using Trackwane.Tests;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Drivers
{
    internal class FindDriverByIdTests : Scenario
    {
        private string DRIVER_ID;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            DRIVER_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Drivers_By_Id()
        {
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID);

            var driver = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(DRIVER_ID);
            driver.ShouldNotBeNull();
        }
    }
}
