using Trackwane.Framework.Common;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _ArchiveLocation
        {
            public static void With(UserClaims persona, string organizationKey, string key) =>
                Client.Use(persona).ArchiveLocation(organizationKey, key);

        }
    }
}
