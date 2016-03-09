using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Vehicles;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Vehicles
{
    internal class AssignTrackerToVehicleTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string VEHICLE_KEY;
        private string USER_KEY;
        private string TRACKER_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            VEHICLE_KEY = Guid.NewGuid().ToString();
            TRACKER_KEY = Guid.NewGuid().ToString();
            USER_KEY = Guid.NewGuid().ToString();
            
            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_KEY);
        }

    
        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Tracker.With(Persona.SystemManager(), TRACKER_KEY, ORGANIZATION_KEY);
            _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
            _Assign_Tracker_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, TRACKER_KEY);

            var vehicle = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(VEHICLE_KEY);
            vehicle.TrackerId.ShouldBe(TRACKER_KEY);
        }

        [Test]
        public void Throws_Exception_When_Tracker_Is_Archived()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Tracker.With(Persona.SystemManager(), TRACKER_KEY, ORGANIZATION_KEY);
                _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _Archive_Tracker.With(Persona.SystemManager(), ORGANIZATION_KEY, TRACKER_KEY);
                _Assign_Tracker_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, TRACKER_KEY);
            });
        }

        [Test]
        public void Throws_Exception_When_Vehicle_Is_Archived()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Tracker.With(Persona.SystemManager(), TRACKER_KEY, ORGANIZATION_KEY);
                _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _Archive_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _Assign_Tracker_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, TRACKER_KEY);
            });
        }

        [Test]
        public void Throws_Exception_When_Vehicle_Is_Unknown()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Tracker.With(Persona.SystemManager(), TRACKER_KEY, ORGANIZATION_KEY);
                _Assign_Tracker_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), TRACKER_KEY);
            });
        }

        [Test]
        public void Throws_Exception_When_Tracker_Is_Unknown()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY);
                _Assign_Tracker_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, Guid.NewGuid().ToString());
            });
        }

        [Test]
        public void Throws_Exception_When_Vehicle_And_Tracker_In_Different_Organizations()
        {
            var OTHER_ORGANIZATION_KEY = Guid.NewGuid().ToString();

            Assert.Throws<BusinessRuleException>(() =>
            {
                _Organization_Registered.With(OTHER_ORGANIZATION_KEY);
                _Register_Tracker.With(Persona.SystemManager(), TRACKER_KEY, ORGANIZATION_KEY);
                _Register_Vehicle.With(Persona.SystemManager(), OTHER_ORGANIZATION_KEY, VEHICLE_KEY);
                _Assign_Tracker_To_Vehicle.With(Persona.SystemManager(), ORGANIZATION_KEY, VEHICLE_KEY, TRACKER_KEY);
            });
        }
    }
}
