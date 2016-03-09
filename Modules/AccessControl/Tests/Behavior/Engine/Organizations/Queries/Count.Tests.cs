using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Engine.Queries.Organizations;
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
            ORGANIZATION_A_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_B_KEY = Guid.NewGuid().ToString();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_A_KEY);
            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_B_KEY);
        }

        [Test]
        public void Counts_All_Organizations_In_System()
        {
            var num = EngineHost.ExecutionEngine.Query<Count>().Execute();
            num.ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}
