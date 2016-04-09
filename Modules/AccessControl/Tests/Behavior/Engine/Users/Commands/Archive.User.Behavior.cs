﻿using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.AccessControl.Engine.Queries.Users;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;

namespace Trackwane.AccessControl.Tests.Behavior.Engine.Users.Commands
{
    internal class Archive_User_Tests : Scenario
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
        public void When_Successful_Publishes_Event()
        {
            _Archive_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);

            WasPosted<UserArchived>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Archive_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);

            EngineHost.ExecutionEngine.Query<FindByKey>().Execute(USER_KEY).IsArchived.ShouldBeTrue();
        }

        [Test]
        public void Can_Be_Executed_By_System_Managers()
        {
            Should.NotThrow(() =>
            {
                _Archive_User.With(Persona.SystemManager(), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Viewers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                _Archive_User.With(Persona.Viewer(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }

        [Test]
        public void Cannot_Be_Executed_By_Managers()
        {
            Should.Throw<UnauthorizedException>(() =>
            {
                _Archive_User.With(Persona.Manager(ORGANIZATION_KEY), ORGANIZATION_KEY, USER_KEY);
            });
        }
    }
}
