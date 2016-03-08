using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Queries.Vehicles;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Vehicles
{
    internal class UpdateVehicleTests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;
        private string VEHICLE_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = "organization-" + Guid.NewGuid();
            VEHICLE_KEY = "vehicle-" + Guid.NewGuid();
            USER_KEY = "user-" + Guid.NewGuid();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_KEY);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
            _Update_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, "A NEW IDENTIFIER");

            var vehicle = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(VEHICLE_KEY);
            vehicle.Identifier.ShouldBe("A NEW IDENTIFIER");
        }
    }
}
