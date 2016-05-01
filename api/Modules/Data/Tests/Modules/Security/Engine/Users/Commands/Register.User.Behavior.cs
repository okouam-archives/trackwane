using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.Tests.Behavior.Engine.Users.Commands
{
    internal class Register_User_Tests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;
        private string OTHER_ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            USER_KEY = GenerateKey();
            ORGANIZATION_KEY = GenerateKey();
            OTHER_ORGANIZATION_KEY = GenerateKey();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            Register_Organization.With(Persona.SystemManager(), OTHER_ORGANIZATION_KEY);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "John Smith", "john.smith@nowhere.com");

            var user = Client.Use(Persona.SystemManager()).Users.FindByKey(USER_KEY);

            Assert.That(user["DisplayName"], Is.EqualTo("John Smith"));
            Assert.That(user["Email"], Is.EqualTo("john.smith@nowhere.com"));
            Assert.That(user["ParentOrganizationKey"], Is.EqualTo(ORGANIZATION_KEY));
        }

        [Test]
        public void Can_Be_Executed_By_Administrators_Within_Administered_Organizations()
        {
            Should.NotThrow(() =>
            {
                Register_User.With(Persona.Administrator(ApplicationKey, ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Administrators_Outside_Administered_Organizations()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_User.With(Persona.Administrator(ApplicationKey, OTHER_ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Can_Be_Executed_By_System_Managers()
        {
            Should.NotThrow(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Viewers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_User.With(Persona.Viewer(ApplicationKey, ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Managers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_User.With(Persona.Manager(ApplicationKey, ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Register_Using_An_Existing_Email()
        {
            var EMAIL = Guid.NewGuid() + "@nowhere.com";

            Assert.Throws<BusinessRuleException>(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, GenerateKey(), "John Smith", EMAIL);
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, GenerateKey(), "Alex Smith", EMAIL);
            });
        }

        [Test]
        public void Throws_A_Validation_Exception_If_No_Password_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "John Smith", Guid.NewGuid() + "@nowhere.com", null);
            });
        }

        [Test]
        public void Throws_A_Validation_Exception_If_Blank_Email_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "John Smith", string.Empty);
            });
        }

        [Test]
        public void Throws_A_Validation_Exception_If_Blank_Password_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "John Smith", Guid.NewGuid() + "@nowhere.com", string.Empty);
            });
        }

        [Test]
        public void Throws_A_Validation_Exception_If_Blank_Display_Name_Provided()
        {
           Assert.Throws<ValidationException>(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, string.Empty);
            });
        }

        [Test]
        public void Throws_A_Validation_Exception_If_No_Display_Name_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, null);
            });
        }
    }
}
