using Trackwane.Framework.Common;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _Assign_Driver_To_Vehicle
        {
            public static void With(UserClaims claims, string organizationId, string vehicleId, string driverId)
            {
                Client.Use(claims).Vehicles.AssignDriver(organizationId, vehicleId, driverId);
            }
        }
    }
}
