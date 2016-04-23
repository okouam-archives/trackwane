using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Vehicles;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Vehicles
{
    internal class AssignDriverToVehicleTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string VEHICLE_KEY;
        private string DRIVER_KEY;
        private string USER_ID;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = GenerateKey();
            VEHICLE_KEY = GenerateKey();
            DRIVER_KEY = GenerateKey();
            USER_ID = GenerateKey();

            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
            _User_Registered.With(ApplicationKey, USER_ID);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_KEY, "ALLEN PAUL");
            _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
            _Assign_Driver_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, DRIVER_KEY);

            var vehicle = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(VEHICLE_KEY);

            vehicle.DriverId.ShouldBe(DRIVER_KEY);
            vehicle.DriverName.ShouldBe("ALLEN PAUL");
        }
      
        [Test]
        public void Throws_Exception_When_Driver_Is_Archived()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_KEY);
                _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _ArchiveDriver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_KEY);
                _Assign_Driver_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, DRIVER_KEY);
            });
        }

        [Test]
        public void Throws_Exception_When_Vehicle_Is_Archived()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_KEY);
                _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _Archive_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _Assign_Driver_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, DRIVER_KEY);
            });
        }
    
        [Test]
        public void Throws_Exception_When_Vehicle_Is_Unknown()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_KEY);
                _Assign_Driver_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), DRIVER_KEY);
            });
        }

        [Test]
        public void Throws_Exception_When_Driver_Is_Unknown()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _Assign_Driver_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, Guid.NewGuid().ToString());
            });
        }

        [Test]
        public void Throws_Exception_When_Driver_And_Vehicle_In_Different_Organizations()
        {
            var OTHER_ORGANIZATION_KEY = Guid.NewGuid().ToString();

            Assert.Throws<BusinessRuleException>(() =>
            {
                _Organization_Registered.With(ApplicationKey, OTHER_ORGANIZATION_KEY);
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_KEY);
                _Register_Vehicle.With(Persona.SystemManager(), OTHER_ORGANIZATION_KEY, VEHICLE_KEY);
                _Assign_Driver_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, DRIVER_KEY);
            });
        }
    }
}
