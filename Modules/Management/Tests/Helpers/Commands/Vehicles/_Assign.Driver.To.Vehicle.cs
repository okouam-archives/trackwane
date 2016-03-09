using Trackwane.Framework.Common;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Assign_Driver_To_Vehicle
        {
            public static void With(UserClaims claims, string organizationId, string vehicleId, string driverId)
            {
                Client.Use(claims).Vehicles.AssignDriverToVehicle(organizationId, vehicleId, driverId);
            }
        }
    }
}
