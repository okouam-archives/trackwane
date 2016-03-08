using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Events;
using Trackwane.Management.Queries.Drivers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Drivers
{
    internal class RegisterDriverTests : Scenario
    {
        private string USER_ID;
        private string ORGANIZATION_KEY;
        private string DRIVER_ID;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            DRIVER_ID = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void On_Success_Publishes_Driver_Registered_Event()
        {
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID);

            WasPosted<DriverRegistered>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID);
            var driver = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(DRIVER_ID);
            driver.ShouldNotBeNull();
        }

        [Test]
        public void Can_Be_Executed_By_System_Administrators()
        {
            Should.NotThrow(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID);
            });
        }
        
        [Test]
        public void Throws_A_Validation_Exception_When_The_Driver_Name_Provided_Is_Blank()
        {
            Assert.Throws<ValidationException>(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID, string.Empty);
            });
        }

        [Test]
        public void Throws_A_Validation_Exception_When_No_Driver_Name_Is_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, DRIVER_ID, null);
            });
        }

        [Test]
        public void Throws_Validation_Exception_The_Driver_Name_Is_A_Duplicate_In_The_Same_Organization()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "John Smith");
                _Register_Driver.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "John Smith");
            });
        }
        
        [Test]
        public void Throws_An_Exception_When_The_Organization_Is_Unknown()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Driver.With(Persona.SystemManager(), Guid.NewGuid().ToString(), DRIVER_ID);
            });
        }
    }
}
