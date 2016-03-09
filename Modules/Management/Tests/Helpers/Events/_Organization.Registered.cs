using System;
using Trackwane.AccessControl.Events;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Organization_Registered
        {
            public static void With(string organizationKey)
            {
                EngineHost.ExecutionEngine.Publish(new OrganizationRegistered
                {
                    OrganizationKey = organizationKey,
                    Name = Guid.NewGuid().ToString()
                });
            }
        }
    }
}
