using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Queries.Vehicles;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Vehicles
{
    internal class ArchiveVehicleTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string VEHICLE_KEY;
        private string USER_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_KEY = "user-" + Guid.NewGuid();
            VEHICLE_KEY = "vehicle-" + Guid.NewGuid();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_KEY);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
            _Archive_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);

            var vehicle = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(VEHICLE_KEY);
            vehicle.IsArchived.ShouldBeTrue();
        }
    }
}
