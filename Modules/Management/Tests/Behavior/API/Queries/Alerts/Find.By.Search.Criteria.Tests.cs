using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Alerts;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Alerts
{
    internal class FindBySearchCriteriaTests : Scenario
    {
        private string ALERT_ID;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            ALERT_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ORGANIZATION_KEY);
        }

        [Test]
        public void Find_Alerts_By_ORGANIZATION_KEY()
        {
            _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_ID);

            var responsePage = EngineHost.ExecutionEngine.Query<FindBySearchCriteria>(ORGANIZATION_KEY).Execute();

            responsePage.Total.ShouldBe(1);
        }
    }
}
