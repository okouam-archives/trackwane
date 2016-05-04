using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Trackers;
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
            ORGANIZATION_KEY = GenerateKey();
            USER_ID = GenerateKey();
            TRACKER_KEY = GenerateKey();

            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
            _User_Registered.With(ApplicationKey, USER_ID);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Tracker.With(Persona.SystemManager(), TRACKER_KEY, ORGANIZATION_KEY);

            var tracker = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(TRACKER_KEY);
            tracker.ShouldNotBeNull();
        }
    }
}
