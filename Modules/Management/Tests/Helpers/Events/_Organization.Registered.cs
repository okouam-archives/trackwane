using Trackwane.AccessControl.Contracts.Events;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Organization_Registered
        {
            public static void With(string applicationKey, string organizationKey)
            {
                Setup.EngineHost.ExecutionEngine.Handle(new OrganizationRegistered {OrganizationKey = organizationKey, ApplicationKey = applicationKey });
            }
        }
    }
}
