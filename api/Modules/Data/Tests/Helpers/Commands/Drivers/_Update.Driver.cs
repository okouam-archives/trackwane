using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _Update_Driver
        {
            public static void With(UserClaims claims, string organizationId, string driverId, string name)
            {
                Client.Use(claims).Vehicles.UpdateDriver(organizationId, driverId, new UpdateDriverModel {Name = name});
            }
        }
    }
}
