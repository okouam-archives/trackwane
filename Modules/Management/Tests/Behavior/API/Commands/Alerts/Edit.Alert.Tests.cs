using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Engine.Queries.Alerts;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Alerts
{
    internal class EditAlertTests : Scenario
    {
        private string ORGANIZATION_KEY;
        private string USER_ID;
        private string ALERT_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();
            ALERT_KEY = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Publishes_Alert_Edited_Event()
        {
            _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, "HERE BE DRAGONS");
            _Edit_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, "NO MORE DRAGONS");

            WasPosted<AlertEdited>().ShouldBeTrue();
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, "HERE BE DRAGONS");
            _Edit_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, "NO MORE DRAGONS");

            var alert = EngineHost.ExecutionEngine.Query<FindByKey>(ORGANIZATION_KEY).Execute(ALERT_KEY);
            alert.Name.ShouldBe("NO MORE DRAGONS");
        }
        
        [Test]
        public void Throws_Exception_If_Name_Provided_Is_Already_In_Use_Within_Organization()
        {
            Assert.Throws<BusinessRuleException>(() =>
            {
                _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, "A");
                _Create_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, Guid.NewGuid().ToString(), "B");
                _Edit_Alert.With(Persona.SystemManager(), ORGANIZATION_KEY, ALERT_KEY, "B");
            });
        }
    }
}
