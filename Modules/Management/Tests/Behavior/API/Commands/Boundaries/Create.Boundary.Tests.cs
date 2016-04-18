using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Engine.Queries.Boundaries;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Boundaries
{
    internal class CreateBoundaryTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string USER_ID;
        private string BOUNDARY_ID;

        [SetUp]
        public void SetUp()
        {
            BOUNDARY_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Create_Boundary.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, BOUNDARY_ID);

            var boundary = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(BOUNDARY_ID);
            boundary.ShouldNotBeNull();
        }

        [Test]
        public void Throws_Exception_If_The_Boundary_ID_Already_Exists()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Create_Boundary.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, BOUNDARY_ID, "ABC");
                _Create_Boundary.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, BOUNDARY_ID, "DAX");
            });
        }

        [Test]
        public void When_Successful_Publishes_Boundary_Created_Event()
        {
            _Create_Boundary.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, BOUNDARY_ID);

            WasPosted<BoundaryCreated>().ShouldBeTrue();
        }

        [Test]
        public void Throws_Domain_Validation_Exception_If_No_Name_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                _Create_Boundary.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, BOUNDARY_ID, string.Empty);
            });
        }

        [Test]
        public void Throws_Exception_If_Name_Already_In_Use()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Create_Boundary.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, BOUNDARY_ID, "MY NAME");
                _Create_Boundary.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "MY NAME");
            });
        }
        
        [Test]
        public void Throws_Domain_Validation_Exception_If_No_Coordinates_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                _Create_Boundary.With(Persona.SystemManager(ApplicationKey), BOUNDARY_ID, "MY NAME", ORGANIZATION_KEY, null);
            });
        }
    }
}
