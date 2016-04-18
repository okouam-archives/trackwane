using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Engine.Queries.Organizations;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Organizations.Commands
{
    internal class Update_Organization_Tests : Scenario
    {
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            var newName = string.Format("new name {0}", ORGANIZATION_KEY);
            Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, string.Format("original name {0}", ORGANIZATION_KEY));
            Update_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, newName);

            EngineHost.ExecutionEngine.Query<FindByKey>(ApplicationKey, ORGANIZATION_KEY).Execute().Name.ShouldBe(newName);
        }

        [Test]
        public void Can_Be_Executed_By_System_Managers()
        {
            Should.NotThrow(() =>
            {
                var newName = string.Format("new name {0}", ORGANIZATION_KEY);
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, string.Format("original name {0}", ORGANIZATION_KEY));
                Update_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, newName);
            });
        }

        [Test]
        public void Can_Be_Executed_By_Administrators_Of_Organization()
        {
            Should.NotThrow(() =>
            {
                var newName = String.Format("new name {0}", ORGANIZATION_KEY);
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, String.Format("original name {0}", ORGANIZATION_KEY));
                Update_Organization.With(Persona.Administrator(ApplicationKey, ORGANIZATION_KEY), ORGANIZATION_KEY, newName);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Administrators_Of_Other_Organizations()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                var newName = String.Format("new name {0}", ORGANIZATION_KEY);
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY,
                    String.Format("original name {0}", ORGANIZATION_KEY));
                Update_Organization.With(Persona.Administrator(Guid.NewGuid().ToString()), ORGANIZATION_KEY, newName);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Managers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                var newName = String.Format("new name {0}", ORGANIZATION_KEY);
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, String.Format("original name {0}", ORGANIZATION_KEY));
                Update_Organization.With(Persona.Manager(ApplicationKey), ORGANIZATION_KEY, newName);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Viewers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                var newName = String.Format("new name {0}", ORGANIZATION_KEY);
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY,String.Format("original name {0}", ORGANIZATION_KEY));
                Update_Organization.With(Persona.Viewer(ApplicationKey), ORGANIZATION_KEY, newName);
            });
        }

        [Test]
        public void Cannot_Update_An_Organization_Name_To_An_Existing_Name()
        {
            Should.Throw<BusinessRuleException>(() =>
            {
                Register_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, "A");
                Register_Organization.With(Persona.SystemManager(ApplicationKey), Guid.NewGuid().ToString(), "B");
                Update_Organization.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, "B");
            });
        }
    }
}
