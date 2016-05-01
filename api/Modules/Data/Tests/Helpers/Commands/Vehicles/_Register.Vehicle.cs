using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _Register_Vehicle
        {
            public static void With(UserClaims claims, string organizationKey, string key)
            {
                Client.Use(claims).Vehicles
                    .Register(organizationKey,
                        new RegisterVehicleModel {Key = key, Identifier = Guid.NewGuid().ToString()});
            }
        }
    }
}
