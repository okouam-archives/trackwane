using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Engine.Queries.Drivers;
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
            ORGANIZATION_KEY = GenerateKey();
            DRIVER_ID = GenerateKey();
            USER_ID = GenerateKey();

            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
            _User_Registered.With(ApplicationKey, USER_ID);
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
            var driver = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(DRIVER_ID);
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
