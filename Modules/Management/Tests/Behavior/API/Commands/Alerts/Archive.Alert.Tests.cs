using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Alerts
{
    internal class ArchiveAlertTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string USER_ID;
        private string ALERT_ID;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();
            ALERT_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Publishes_Alert_Archived_Event()
        {
            _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_ID);
            _ArchiveAlert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_ID);

            WasPosted<AlertArchived>().ShouldBeTrue();
        }

        [Test]
        public void When_Failed_Does_Not_Publish_Alert_Archived_Event()
        {
            try
            {
                _ArchiveAlert.With(Persona.SystemManager(), ORGANIZATION_KEY, null);
            }
            catch {}
     
            WasPosted<AlertArchived>().ShouldBeFalse();
        }
    }
}
