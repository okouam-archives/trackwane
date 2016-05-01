using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;

namespace Trackwane.Tests.Behavior.Engine.Users.Queries
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
        public void Counts_All_Users_In_Organization()
        {
            var num = Client.Use(Persona.SystemManager()).Users.Count(ORGANIZATION_A_KEY);
            num.ShouldBe(2);
        }

        [Test]
        public void Counts_All_Users_In_System()
        {
            var num = Client.Use(Persona.SystemManager()).Users.Count();
            num.ShouldBeGreaterThanOrEqualTo(3);
        }
    }
}
