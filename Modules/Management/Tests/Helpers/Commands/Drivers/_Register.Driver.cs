using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Register_Driver
        {
            public static void With(UserClaims persona, string organizationKey, string key, string name)
            {
                Client.Use(persona).Vehicles.RegisterDriver(organizationKey, new CreateDriverModel {Key = key, Name = name});
            }

            public static void With(UserClaims persona, string organizationKey, string key)
            {
                With(persona, organizationKey, key, Guid.NewGuid().ToString());
            }

            public static void With(UserClaims persona, string organizationKey)
            {
                With(persona, organizationKey, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
        }
    }
}
