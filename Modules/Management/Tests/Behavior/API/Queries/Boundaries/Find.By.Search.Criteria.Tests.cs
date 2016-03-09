using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Boundaries;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Boundaries
{
    internal class FindBySearchCriteriaTests : Scenario
    {
        private string BOUNDARY_ID;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            BOUNDARY_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Boundaries_When_Searching_By_Organization()
        {
            _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID);

            var responsePage = EngineHost.ExecutionEngine.Query<FindBySearchCriteria>(ORGANIZATION_KEY).Execute(ORGANIZATION_KEY);

            responsePage.Total.ShouldBe(1); 
        }
    }
}
