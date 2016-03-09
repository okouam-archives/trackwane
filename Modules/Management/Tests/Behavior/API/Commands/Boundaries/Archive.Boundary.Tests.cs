using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Boundaries;
using Trackwane.Management.Events;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Boundaries
{
    internal class ArchiveBoundaryTests : Scenario
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
        public void When_Successful_Publishes_Boundary_Archived_Event()
        {
            _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID);
            _Archive_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID);

            WasPosted<BoundaryArchived>().ShouldBeTrue();
        }

        [Test]
        public void When_Failed_Does_Not_Publish_Boundary_Archived_Event()
        {
            try
            {
                _Archive_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString());
            }
            catch {}
     
            WasPosted<BoundaryArchived>().ShouldBeFalse();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Create_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID);
            _Archive_Boundary.With(Persona.SystemManager(), ORGANIZATION_KEY, BOUNDARY_ID);

            var boundary = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(BOUNDARY_ID);
            boundary.IsArchived.ShouldBeTrue();
        }
    }
}
