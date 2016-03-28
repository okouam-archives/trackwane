using Trackwane.Framework.Common;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Archive_Tracker
        {
            public static void With(UserClaims claims, string organizationId, string trackerId) =>
                Client.Use(claims).ArchiveTracker(organizationId, trackerId);
        }
    }
}
