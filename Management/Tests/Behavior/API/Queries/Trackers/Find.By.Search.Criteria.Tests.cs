using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Queries.Trackers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Trackers
{
    internal class FindBySearchCriteriaTests : Scenario
    {
        private string TRACKER_ID;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            TRACKER_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Trackers_When_Searching_By_Organization()
        {
            _Register_Tracker.With(Persona.SystemManager(), TRACKER_ID, ORGANIZATION_KEY);

            var responsePage = EngineHost.ExecutionEngine.Query<FindBySearchCriteria>(ORGANIZATION_KEY).Execute();

            responsePage.Total.ShouldBe(1);
        }
    }
}
