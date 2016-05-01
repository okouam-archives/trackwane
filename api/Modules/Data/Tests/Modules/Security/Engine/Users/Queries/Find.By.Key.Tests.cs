using System;
using NUnit.Framework;
using Trackwane.Framework.Fixtures;

namespace Trackwane.Tests.Behavior.Engine.Users.Queries
{
    internal class Find_By_Key_Tests : Scenario
    {
        private string USER_1_IN_ORGANIZATION_A_KEY;
        private string USER_2_IN_ORGANIZATION_A_KEY;
        private string USER_IN_ORGANIZATION_B_KEY;
        private string ORGANIZATION_A_KEY;
        private string ORGANIZATION_B_KEY;

        [SetUp]
        public void SetUp()
        {
            USER_1_IN_ORGANIZATION_A_KEY = GenerateKey();
            USER_2_IN_ORGANIZATION_A_KEY = GenerateKey();
            USER_IN_ORGANIZATION_B_KEY = GenerateKey();
            ORGANIZATION_A_KEY = GenerateKey();
            ORGANIZATION_B_KEY = GenerateKey();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_A_KEY);
            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_B_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_A_KEY, USER_1_IN_ORGANIZATION_A_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_A_KEY, USER_2_IN_ORGANIZATION_A_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_B_KEY, USER_IN_ORGANIZATION_B_KEY);
        }

        [Test]
        public void Finds_User_Details_When_User_Exists()
        {
            var user = Client.Use(Persona.SystemManager()).Users.FindByKey(USER_1_IN_ORGANIZATION_A_KEY);
            Assert.That(user["Key"], Is.EqualTo(USER_1_IN_ORGANIZATION_A_KEY));
        }

        [Test]
        public void Finds_Nothing_When_User_Does_Not_Exist()
        {
            var user = Client.Use(Persona.SystemManager()).Users.FindByKey(GenerateKey());
            Assert.That(user, Is.Null);
        }
    }
}
