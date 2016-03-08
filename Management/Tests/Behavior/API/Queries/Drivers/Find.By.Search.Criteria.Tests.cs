using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Queries.Drivers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Drivers
{
    internal class FindBySearchCriteriaTests : Scenario
    {
        private string DRIVER_KEY;
        private string ORGANIZATION_KEY;
        private string ANOTHER_ORGANIZATION_KEY;
        private string ANOTHER_DRIVER_KEY;

        [SetUp]
        public void SetUp()
        {
            DRIVER_KEY = Guid.NewGuid().ToString();
            ANOTHER_DRIVER_KEY = Guid.NewGuid().ToString();
            ANOTHER_ORGANIZATION_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ORGANIZATION_KEY);
            _Organization_Registered.With(ANOTHER_ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Drivers_When_Searching_By_Organization()
        {
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_KEY, "A NAME");
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, ANOTHER_DRIVER_KEY, "ANOTHER NAME");
            _Register_Driver.With(Persona.SystemManager(), ANOTHER_ORGANIZATION_KEY, DRIVER_KEY, "YET ANOTHER NAME");

            var responsePage = EngineHost.ExecutionEngine.Query<FindBySearchCriteria>(ORGANIZATION_KEY).Execute("A NAME");

            responsePage.Total.ShouldBe(1);
        }
    }
}
