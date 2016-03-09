using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Users.Queries
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
            USER_1_IN_ORGANIZATION_A_KEY = Guid.NewGuid().ToString();
            USER_2_IN_ORGANIZATION_A_KEY = Guid.NewGuid().ToString();
            USER_IN_ORGANIZATION_B_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_A_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_B_KEY = Guid.NewGuid().ToString();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_A_KEY);
            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_B_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_A_KEY, USER_1_IN_ORGANIZATION_A_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_A_KEY, USER_2_IN_ORGANIZATION_A_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_B_KEY, USER_IN_ORGANIZATION_B_KEY);
        }

        [Test]
        public void Finds_User_Details_When_User_Exists()
        {
            var user = EngineHost.ExecutionEngine.Query<FindByKey>().Execute(USER_1_IN_ORGANIZATION_A_KEY);
            user.Key.ShouldBe(USER_1_IN_ORGANIZATION_A_KEY);
        }

        [Test]
        public void Finds_Nothing_When_User_Does_Not_Exist()
        {
            var user = EngineHost.ExecutionEngine.Query<FindByKey>().Execute(Guid.NewGuid().ToString());
            user.ShouldBeNull();
        }
    }
}
