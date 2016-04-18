﻿using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Locations;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Locations
{
    internal class RegisterLocationTests : Scenario
    {
        private string USER_ID;
        private string ORGANIZATION_KEY;
        private string LOCATION_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            LOCATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void Can_Be_Executed_By_System_Administrators()
        {
            _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_KEY);

            var location = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(LOCATION_KEY);
            location.ShouldNotBeNull();
        }

        [Test]
        public void Throws_Validation_Exception_If_No_Name_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_KEY, String.Empty);
            });
        }

        [Test]
        public void Throws_Exception_If_No_Coordinates_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                _Register_Location.With(Persona.SystemManager(ApplicationKey), LOCATION_KEY, ORGANIZATION_KEY, "My Location Name", null);
            });
        }

        [Test]
        public void Throws_Exception_If_Name_Already_In_Use()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_KEY, "My Location Name");
                _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "My Location Name");
            });
        }
    }
}
