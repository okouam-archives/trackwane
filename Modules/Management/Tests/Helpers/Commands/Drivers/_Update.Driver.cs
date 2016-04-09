using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Update_Driver
        {
            public static void With(UserClaims claims, string organizationId, string driverId, string name)
            {
                Client.Use(claims).UpdateDriver(organizationId, driverId, new UpdateDriverModel {Name = name});
            }
        }
    }
}
