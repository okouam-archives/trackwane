using Trackwane.Framework.Common;
using Trackwane.Management.Models.Vehicles;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Update_Vehicle
        {
            public static void With(UserClaims claims, string organizationKey, string key, string identifier) =>
                Client.Use(claims).Vehicles.Update(organizationKey, key, new UpdateVehicleModel
                {
                    Identifier = identifier
                });
        }
    }
}
