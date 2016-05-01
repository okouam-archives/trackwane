using Trackwane.Framework.Common;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _ArchiveLocation
        {
            public static void With(UserClaims persona, string organizationKey, string key)
            {
                Client.Use(persona).Locations.Archive(organizationKey, key);
            }
        }
    }
}
