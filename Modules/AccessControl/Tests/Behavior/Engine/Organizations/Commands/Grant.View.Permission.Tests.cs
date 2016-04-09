using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Organizations.Commands
{
    internal class Grant_View_Permission_Tests : Scenario
    {
        private string USER_KEY;
        private string ORGANIZATION_KEY;
        
        [SetUp]
        public void SetUp()
        {
            USER_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();

            Register_Organization.With(Persona.SystemManager(), ORGANIZATION_KEY);
            Register_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
        }

        [Test]
        public void When_Successful_Publishes_Events()
        {
            Grant_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);

            WasPosted<ViewPermissionGranted>().ShouldBeTrue();
        }

        [Test]
        public void Can_Be_Executed_Multiple_Times_For_Same_User()
        {
            Grant_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
            Grant_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);

            var user = EngineHost.ExecutionEngine.Query<FindByKey>().Execute(USER_KEY);

            user.View.Count.ShouldBe(1);
            user.View[0].Item1.ShouldBe(ORGANIZATION_KEY);
        }


        [Test]
        public void Cannot_Be_Executed_By_Viewers()
        {
            Assert.Throws<UnauthorizedException>(() =>
            {
                Grant_View_Permission.With(Persona.Viewer(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Managers()
        {
            Assert.Throws<UnauthorizedException>(() =>
            {
                Grant_View_Permission.With(Persona.Manager(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Can_Be_Executed_By_System_Managers()
        {
            Assert.DoesNotThrow(() =>
            {
                Grant_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Can_Be_Executed_By_Administrators()
        {
            Assert.DoesNotThrow(() =>
            {
                Grant_View_Permission.With(Persona.Administrator(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Removes_Other_Permissions_In_Organization_For_User()
        {
            Grant_Manage_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
            Grant_View_Permission.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);

            var user = EngineHost.ExecutionEngine.Query<FindByKey>().Execute(USER_KEY);

            user.Manage.ShouldBeEmpty();
        }
    }
}
