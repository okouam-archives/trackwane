using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Organizations.Commands
{
    internal class Register_Organization_Tests : Scenario
    {
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = GenerateKey();
        }

        [Test]
        public void Cannot_Be_Executed_By_User_Without_Permissions()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_Organization.With(Persona.User(), ORGANIZATION_KEY);
            });
        }

        [Test]
        public void When_Successful_Publishes_Events()
        {
            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            WasPosted<OrganizationRegistered>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            var organization = Client.Use(Persona.SystemManager()).Organizations.FindByKey(ORGANIZATION_KEY);
            organization.ShouldNotBeNull();
        }

        [Test]
        public void Throws_Exception_When_The_Organization_ID_Provided_Is_Already_In_Use()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
                Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            });
        }

        [Test]
        public void Throws_Exception_When_No_Organization_Name_Is_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY, null);
            });
        }

        [Test] 
        public void Throws_Exception_When_The_Organization_Name_Provided_Is_Already_In_Use()
        {
            var ORGANIZATION_KEY_A = GenerateKey();
            var ORGANIZATION_KEY_B = GenerateKey();

            Assert.Throws<BusinessRuleException>(() =>
            {
                Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY_A,
                    string.Format("Test Org {0}", ORGANIZATION_KEY_A));
                Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY_B,
                    string.Format("Test Org {0}", ORGANIZATION_KEY_A));
            });
        }
    }
}
