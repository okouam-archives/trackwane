using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Commands.Drivers;
using Trackwane.Management.Responses.Drivers;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Update_Driver
        {
            public static void With(UserClaims claims, string organizationId, string driverId, string name)
            {
                Client.Use(claims).Drivers.Update(organizationId, driverId, new UpdateDriverModel
                {
                    Name = name
                });
            }
        }
    }
}
