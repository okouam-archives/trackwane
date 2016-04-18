using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Drivers;
using Trackwane.Management.Tests.Helpers;

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
            _Organization_Registered.With(ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Drivers_By_Id()
        {
            _Register_Driver.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, DRIVER_ID);

            var driver = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(DRIVER_ID);
            driver.ShouldNotBeNull();
        }

    }
}
