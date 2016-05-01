using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.Framework.Fixtures;

namespace Trackwane.Tests.Behavior.Engine.Organizations.Commands
{
    internal class Revoke_View_Permission_Tests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            USER_KEY = GenerateKey();
            ORGANIZATION_KEY = GenerateKey();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
            Grant_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
        }

        [Test]
        public void When_Successful_Publishes_Event()
        {
            Revoke_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);

            WasPosted<ViewPermissionRevoked>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Change()
        {
            Revoke_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
            var organization = Client.Use(Persona.SystemManager()).Organizations.FindByKey(ORGANIZATION_KEY);
            organization.Viewers.ShouldBeEmpty();
        }
    }
}
