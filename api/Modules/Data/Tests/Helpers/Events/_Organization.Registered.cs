using Trackwane.AccessControl.Contracts.Events;
using Trackwane.Tests;
using Trackwane.Tests;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _Organization_Registered
        {
            public static void With(string applicationKey, string organizationKey)
            {
                Setup.EngineHost.ExecutionEngine.Publish(new OrganizationRegistered {OrganizationKey = organizationKey, ApplicationKey = applicationKey });
            }
        }
    }
}
