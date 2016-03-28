using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Register_Vehicle
        {
            public static void With(UserClaims claims, string organizationKey, string key) =>
                Client.Use(claims).RegisterVehicle(organizationKey, new RegisterVehicleModel(key, Guid.NewGuid().ToString()));

        }
    }
}
