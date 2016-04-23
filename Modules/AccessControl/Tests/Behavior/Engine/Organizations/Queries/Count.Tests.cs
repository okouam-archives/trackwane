using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Organizations.Queries
{
    internal class Count_Tests : Scenario
    {
        private string ORGANIZATION_A_KEY;
        private string ORGANIZATION_B_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_A_KEY = GenerateKey();
            ORGANIZATION_B_KEY = GenerateKey();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_A_KEY);
            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_B_KEY);
        }

        [Test]
        public void Counts_All_Organizations_In_System()
        {
            var num = Client.Use(Persona.SystemManager()).Organizations.Count();
            num.ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}
