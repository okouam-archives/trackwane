using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Boundaries;
using Trackwane.Tests;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Boundaries
{
    internal class FindByIdTests : Scenario
    {
        private string BOUNDARY_KEY;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            BOUNDARY_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
        }

        [Test]
        public void Finds_Boundaries_By_Id()
        {
            _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_KEY);

            var boundary = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(BOUNDARY_KEY);
            boundary.ShouldNotBeNull();
        }
    }
}
