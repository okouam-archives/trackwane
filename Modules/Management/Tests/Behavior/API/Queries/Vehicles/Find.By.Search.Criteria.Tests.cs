using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Engine.Queries.Vehicles;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Vehicles
{
    internal class FindBySearchCriteriaTests : Scenario
    {
        private string VEHICLE_ID;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            VEHICLE_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Vehicles_When_Searching_Organization_Without_Filters()
        {
            _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_ID);

            var responsePage = Setup.EngineHost.ExecutionEngine.Query<FindBySearchCriteria>(ApplicationKey, ORGANIZATION_KEY).Execute(new SearchVehiclesModel());

            responsePage.Total.ShouldBe(1);
        }
    }
}
