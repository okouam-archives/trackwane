using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Users.Queries
{
    internal class Count_Tests : Scenario
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

            Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_A_KEY);
            Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_B_KEY);
            Register_User.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_A_KEY, USER_1_IN_ORGANIZATION_A_KEY);
            Register_User.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_A_KEY, USER_2_IN_ORGANIZATION_A_KEY);
            Register_User.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_B_KEY, USER_IN_ORGANIZATION_B_KEY);
        }

        [Test]
        public void Counts_All_Users_In_Organization()
        {
            var num = EngineHost.ExecutionEngine.Query<Count>(ApplicationKey).Execute(ORGANIZATION_A_KEY);
            num.ShouldBe(2);
        }

        [Test]
        public void Counts_All_Users_In_System()
        {
            var num = EngineHost.ExecutionEngine.Query<Count>(ApplicationKey).Execute();
            num.ShouldBeGreaterThanOrEqualTo(3);
        }
    }
}
