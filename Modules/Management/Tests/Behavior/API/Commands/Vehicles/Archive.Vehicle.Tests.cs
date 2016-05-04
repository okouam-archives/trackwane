using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Vehicles;
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
            ORGANIZATION_KEY = GenerateKey();
            USER_KEY = GenerateKey();
            VEHICLE_KEY = GenerateKey();

            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
            _User_Registered.With(ApplicationKey, USER_KEY);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
            _Archive_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);

            var vehicle = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(VEHICLE_KEY);
            vehicle.IsArchived.ShouldBeTrue();
        }
    }
}
