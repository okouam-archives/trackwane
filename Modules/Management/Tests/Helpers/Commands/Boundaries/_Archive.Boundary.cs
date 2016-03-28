using Trackwane.Framework.Common;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Archive_Boundary
        {
            public static void With(UserClaims claims, string organizationKey, string key)
            {
                Client.Use(claims).ArchiveBoundary(organizationKey, key);
            }
        }
    }
}
