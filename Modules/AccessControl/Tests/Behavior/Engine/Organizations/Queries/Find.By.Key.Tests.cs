using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Engine.Queries.Organizations;
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
            ORGANIZATION_A_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_B_KEY = Guid.NewGuid().ToString();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_A_KEY);
            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_B_KEY);
        }

        [Test]
        public void Finds_Organization_Details_When_Organization_Exists()
        {
            var organization = EngineHost.ExecutionEngine.Query<FindByKey>(ORGANIZATION_A_KEY).Execute();
            organization.Key.ShouldBe(ORGANIZATION_A_KEY);
        }

        [Test]
        public void Finds_Nothing_When_Organization_Does_Not_Exist()
        {
            var organization = EngineHost.ExecutionEngine.Query<FindByKey>(Guid.NewGuid().ToString()).Execute();
            organization.ShouldBeNull();
        }
    }
}
