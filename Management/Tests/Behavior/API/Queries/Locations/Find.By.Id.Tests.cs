using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Queries.Locations;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Locations
{
    internal class FindByIdTests : Scenario
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
        public void Finds_Locations_By_Id()
        {
            _Register_Location.With(Persona.SystemManager(), ORGANIZATION_KEY, LOCATION_ID);

            var location = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(LOCATION_ID);
            location.ShouldNotBeNull();
        }
    }
}
