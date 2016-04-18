using System;
using NUnit.Framework;
using Shouldly;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Fixtures;
using Trackwane.Management.Engine.Queries.Locations;
using Trackwane.Management.Tests.Helpers;

namespace Trackwane.Management.Tests.Behavior.API.Commands.Locations
{
    internal class UpdateLocationTests : Scenario
    {
        private string USER_ID;
        private string ORGANIZATION_KEY;
        private string LOCATION_KEY;

        [SetUp]
        public void SetUp()
        {
            ORGANIZATION_KEY = Guid.NewGuid().ToString();
            LOCATION_KEY = Guid.NewGuid().ToString();
            USER_ID = Guid.NewGuid().ToString();

            _Organization_Registered.With(ORGANIZATION_KEY);
            _User_Registered.With(USER_ID);
        }

        [Test]
        public void When_Successful_Persists_Changes()
        {
            _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_KEY);
            _Update_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_KEY, "A NEW NAME");
            
            var location = EngineHost.ExecutionEngine.Query<FindById>(ORGANIZATION_KEY).Execute(LOCATION_KEY);
            location.Name.ShouldBe("A NEW NAME");
        }
        
        [Test]
        public void Throws_Exception_When_Name_Provided_Is_Already_In_Use()
        {
            var OTHER_LOCATION_ID = Guid.NewGuid().ToString();

            Should.Throw<BusinessRuleException>(() =>
            {
                _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_KEY, "MY NAME");
                _Register_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, OTHER_LOCATION_ID, "MY OTHER NAME");
                _Update_Location.With(Persona.SystemManager(ApplicationKey), ORGANIZATION_KEY, LOCATION_KEY, "MY OTHER NAME");
            });
        }
    }
}
