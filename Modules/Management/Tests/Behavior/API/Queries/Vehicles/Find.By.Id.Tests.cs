using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Vehicles;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Vehicles
{
    internal class FindVehicleByIdTests : Scenario
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
        public void Finds_Vehicles_By_Id()
        {
            _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_ID);

            var vehicle = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(VEHICLE_ID);
            vehicle.ShouldNotBeNull();
        }
    }
}
