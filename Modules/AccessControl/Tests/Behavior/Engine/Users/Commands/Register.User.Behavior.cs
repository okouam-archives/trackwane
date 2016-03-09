using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Users.Commands
{
    internal class Register_User_Tests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;
        private string OTHER_ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            USER_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            OTHER_ORGANIZATION_KEY = Guid.NewGuid().ToString();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            Register_Organization.With(Persona.SystemManager(), OTHER_ORGANIZATION_KEY);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "John Smith", "john.smith@nowhere.com");

            var user = EngineHost.ExecutionEngine.Query<FindByKey>().Execute(USER_KEY);
            user.DisplayName.ShouldBe("John Smith");
            user.Email.ShouldBe("john.smith@nowhere.com");
            user.ParentOrganizationKey.ShouldBe(ORGANIZATION_KEY);
        }

        [Test]
        public void Can_Be_Executed_By_Administrators_Within_Administered_Organizations()
        {
            Should.NotThrow(() =>
            {
                Register_User.With(Persona.Administrator(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Administrators_Outside_Administered_Organizations()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_User.With(Persona.Administrator(OTHER_ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
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
                Register_User.With(Persona.Viewer(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Managers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_User.With(Persona.Manager(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Register_Using_An_Existing_Email()
        {
            var EMAIL = Guid.NewGuid() + "@nowhere.com";

            Assert.Throws<BusinessRuleException>(() =>
            {
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "John Smith", EMAIL);
                Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "Alex Smith", EMAIL);
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
