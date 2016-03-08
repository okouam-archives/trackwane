using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Events;
using Trackwane.Management.Queries.Boundaries;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Boundaries
{
    internal class UpdateBoundaryTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string USER_ID;
        private string BOUNDARY_ID;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();
            BOUNDARY_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Publishes_Boundary_Updated_Event()
        {
            _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID, "HERE BE DRAGONS");
            _Edit_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID, "NO MORE DRAGONS");

            WasPosted<BoundaryUpdated>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID, "HERE BE DRAGONS");
            _Edit_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID, "NO MORE DRAGONS");

            var boundary = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(BOUNDARY_ID);
            boundary.Name.ShouldBe("NO MORE DRAGONS");
        }

        [Test]
        public void Throws_Exception_If_New_Name_Provided_But_Already_In_Use()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID, "A");
                _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "B");
                _Edit_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID, "B");
            });
        }
    }
}
