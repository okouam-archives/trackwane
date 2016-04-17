﻿using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Trackers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Trackers
{
    internal class ArchiveTrackerTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string USER_ID;
        private string TRACKER_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();
            TRACKER_KEY = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Tracker.With(Persona.SystemManager(ApplicationKey), TRACKER_KEY, ORGANIZATION_KEY);
            _Archive_Tracker.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, TRACKER_KEY);

            var tracker = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(TRACKER_KEY);
            tracker.IsArchived.ShouldBeTrue();
        }
    }
}
