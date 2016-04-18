using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.AccessControl.Engine.Queries.Organizations;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Organizations.Commands
{
    internal class Archive_Organization_Tests : Scenario
    {
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
        }

        [Test]
        public void Cannot_Be_Executed_By_Administrators()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);
                Archive_Organization.With(Persona.Administrator(ORGANIZATION_KEY), ORGANIZATION_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Managers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);
                Archive_Organization.With(Persona.Manager(ORGANIZATION_KEY), ORGANIZATION_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Viewers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);
                Archive_Organization.With(Persona.Viewer(ORGANIZATION_KEY), ORGANIZATION_KEY);
            });
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);
            Archive_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);

            EngineHost.ExecutionEngine.Query<FindByKey>(ApplicationKey, ORGANIZATION_KEY).Execute().IsArchived.ShouldBeTrue();
        }

        [Test]
        public void Can_Be_Executed_By_System_Managers()
        {
            Should.NotThrow(() =>
            {
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);
                Archive_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);
            });
        }

        [Test]
        public void When_Successful_Publishes_Event()
        {
            Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);
            Archive_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY);

            WasPosted<OrganizationArchived>().ShouldBeTrue();
        }
    }
}
