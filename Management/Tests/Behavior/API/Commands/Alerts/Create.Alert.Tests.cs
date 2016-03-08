using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Events;
using Trackwane.Management.Queries.Alerts;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Alerts
{
    internal class CreateAlertTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string ALERT_KEY;
        private string USER_ID;

        [SetUp]
        public void SetUp()
        {
            ALERT_KEY = Guid.NewGuid().ToString();
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY);

            EngineHost.ExecutionEngine.Query<FindByKey>(ALERT_KEY).ShouldNotBeNull();
        }

        [Test]
        public void When_Successful_Publishes_Alert_Created_Event()
        {
            _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY);

            WasPosted<AlertCreated>().ShouldBeTrue();
        }

        [Test]
        public void Throws_Exception_If_No_Name_Provided()
        {
            Assert.Throws<ValidationException>(() =>
            {
                _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, string.Empty);
            });
        }

        [Test]
        public void Throws_Exception_If_Name_Already_In_Use_Within_Organization()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, "MY NAME");
                _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "MY NAME");
            });
        }
    }
}
