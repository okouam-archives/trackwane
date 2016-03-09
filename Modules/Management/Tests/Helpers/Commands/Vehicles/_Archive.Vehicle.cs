using Trackwane.Framework.Common;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Archive_Vehicle
        {
            public static void With(UserClaims claims, string organizationId, string vehicleId) =>
                Client.Use(claims).Vehicles.Archive(organizationId, vehicleId);
        }
    }
}
