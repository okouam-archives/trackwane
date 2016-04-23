using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Organizations.Queries
{
    internal class Find_By_Key_Tests : Scenario
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
        public void Finds_Organization_Details_When_Organization_Exists()
        {
            var organization = Client.Use(Persona.SystemManager()).Organizations.FindByKey(ORGANIZATION_A_KEY);
            organization.Key.ShouldBe(ORGANIZATION_A_KEY);
        }

        [Test]
        public void Finds_Nothing_When_Organization_Does_Not_Exist()
        {
            var organization = Client.Use(Persona.SystemManager()).Organizations.FindByKey(GenerateKey());
            organization.ShouldBeNull();
        }
    }
}
