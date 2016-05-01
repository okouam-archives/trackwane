using Trackwane.Framework.Common;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _ArchiveAlert
        {
            public static void With(UserClaims claims, string organizationKey, string key)
            {
                Client.Use(claims).Alerts.Archive(organizationKey, key);
            }
        }
    }
}