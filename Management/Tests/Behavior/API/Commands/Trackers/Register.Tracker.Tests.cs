using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Queries.Trackers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Trackers
{
    internal class RegisterTrackerTests : Scenario
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
            _Register_Tracker.With(Persona.SystemManager(), TRACKER_KEY, ORGANIZATION_KEY);

            var tracker = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(TRACKER_KEY);
            tracker.ShouldNotBeNull();
        }
    }
}
