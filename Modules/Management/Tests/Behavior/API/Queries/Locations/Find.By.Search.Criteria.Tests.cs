using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Locations;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Locations
{
    internal class FindBySearchCriteriaTests : Scenario
    {
        private string LOCATION_ID;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            LOCATION_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Locations_When_Searching_By_Organization()
        {
            _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_ID);

            var responsePage = EngineHost.ExecutionEngine.Query<FindBySearchCriteria>(ORGANIZATION_KEY).Execute();

            responsePage.Total.ShouldBe(1);
        }
    }
}
