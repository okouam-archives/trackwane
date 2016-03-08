using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Events;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Events;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Locations
{
    internal class ArchiveLocationTests : Scenario
    {
        private string USER_ID;
        private string ORGANIZATION_KEY;
        private string LOCATION_ID;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            LOCATION_ID = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void On_Success_Publishes_Location_Archived_Event()
        {
            _Register_Location.With(Persona.SystemManager(), ORGANIZATION_KEY, LOCATION_ID);
            _ArchiveLocation.With(Persona.SystemManager(), ORGANIZATION_KEY, LOCATION_ID);

            WasPosted<LocationArchived>().ShouldBeTrue();
        }

        [Test]
        public void Can_Be_Executed_By_System_Administrators()
        {
            Should.NotThrow(() =>
            {
                _Register_Location.With(Persona.SystemManager(), ORGANIZATION_KEY, LOCATION_ID);
                _ArchiveLocation.With(Persona.SystemManager(), ORGANIZATION_KEY, LOCATION_ID);
            });
        }
    }
}
