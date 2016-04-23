using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Users.Queries
{
    internal class Get_Access_Token_Tests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;
        private string EMAIL;
        private string PASSWORD;

        [SetUp]
        public void SetUp()
        {
            USER_KEY = GenerateKey();
            ORGANIZATION_KEY = GenerateKey();
            EMAIL = GenerateKey();
            PASSWORD = GenerateKey();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY,
                GenerateKey(), EMAIL, PASSWORD);
        }

        [Test]
        public void Gets_Access_Token_When_User_Exists()
        {
            Client.UseWithoutAuthentication().Users.GetAccessToken(EMAIL, PASSWORD).ShouldBeNull();
        }

        [Test]
        public void Finds_Nothing_When_User_With_Correct_Email_But_Different_Password_Exists()
        {
            Client.UseWithoutAuthentication()
                .Users.GetAccessToken(EMAIL, GenerateKey())
                .ShouldBeNull();
        }

        [Test]
        public void Finds_Nothing_When_User_With_Correct_Password_But_Different_Email_Exists()
        {
            Client.UseWithoutAuthentication()
                .Users.GetAccessToken(GenerateKey(), PASSWORD)
                .ShouldBeNull();
        }
    }
}
