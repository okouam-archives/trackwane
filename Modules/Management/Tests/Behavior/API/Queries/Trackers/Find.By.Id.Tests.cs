using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Trackers;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Queries.Trackers
{
    internal class FindByIdTests : Scenario
    {
        private string TRACKER_ID;
        private string ORGANIZATION_KEY;

        [SetUp]
        public void SetUp()
        {
            TRACKER_ID = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            _Organization_Registered.With(ApplicationKey, ORGANIZATION_KEY);
        }
        
        [Test]
        public void Finds_Trackers_By_Id()
        {
            _Register_Tracker.With(Persona.SystemManager(), TRACKER_ID, ORGANIZATION_KEY);

            var tracker = Setup.EngineHost.ExecutionEngine.Query<FindById>(ApplicationKey, ORGANIZATION_KEY).Execute(TRACKER_ID);

            tracker.ShouldNotBeNull();
        }
    }
}
