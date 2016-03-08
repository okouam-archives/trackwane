using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Users.Queries
{
    internal class Find_By_Credentials_Tests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;
        private string EMAIL;
        private string PASSWORD;

        [SetUp]
        public void SetUp()
        {
            USER_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            EMAIL = Guid.NewGuid().ToString();
            PASSWORD = Guid.NewGuid().ToString();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY, Guid.NewGuid().ToString(), EMAIL, PASSWORD);
        }

        [Test]
        public void Finds_User_By_Credentials_When_User_Exists()
        {
            EngineHost.ExecutionEngine.Query<FindByCredentials>().Execute(EMAIL, PASSWORD).ShouldNotBeNull();
        }

        [Test]
        public void Finds_Nothing_When_User_With_Correct_Email_But_Different_Password_Exists()
        {
            EngineHost.ExecutionEngine.Query<FindByCredentials>().Execute(EMAIL, Guid.NewGuid().ToString()).ShouldBeNull();
        }

        [Test]
        public void Finds_Nothing_When_User_With_Correct_Password_But_Different_Email_Exists()
        {
            EngineHost.ExecutionEngine.Query<FindByCredentials>().Execute(Guid.NewGuid().ToString(), PASSWORD).ShouldBeNull();
        }
    }
}
