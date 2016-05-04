using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Users.Commands
{
    internal class Update_User_Tests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            USER_KEY = GenerateKey();
            ORGANIZATION_KEY = GenerateKey();
            Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "John Smith");
        }

        [Test]
        public void When_Successful_Persists_changes()
        {
            Update_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");

            var user = Client.Use(Persona.SystemManager()).Users.FindByKey(USER_KEY);
            Assert.That(user["DisplayName"], Is.EqualTo("Rachel Johnson"));
        }

        [Test]
        public void When_Successful_Publishes_Event()
        {
            Update_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");

            WasPosted<UserUpdated>().ShouldBeTrue();
        }

        [Test]
        public void Can_Be_Executed_By_System_Managers_For_Any_Other_User()
        {
            Should.NotThrow(() =>
            {
                Update_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Administrators_Without_Permission_To_The_Parent_Organization_Of_The_User()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Update_User.With(Persona.Administrator(ApplicationKey, GenerateKey()), ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");
            });
        }

        [Test]
        public void Can_Be_Executed_By_Administrators_With_Permission_To_The_Parent_Organization_Of_The_User()
        {
            Should.NotThrow(() =>
            {
                Update_User.With(Persona.Administrator(ApplicationKey, ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Viewers_For_Other_Users()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Update_User.With(Persona.Viewer(ApplicationKey, ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");
            });
        }
        
        [Test]
        public void Cannot_Be_Executed_By_Managers_For_Other_Users()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Update_User.With(Persona.Manager(ApplicationKey, ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");
            });
        }

        [Test]
        public void Can_Be_Executed_By_Users_When_Updating_Their_Own_Information()
        {
            Should.NotThrow(() =>
            {
                var user = Persona.User();
                user.UserId = USER_KEY;
                Update_User.With(user, ORGANIZATION_KEY, USER_KEY, "Rachel Johnson");
            });
        }
    }
}
