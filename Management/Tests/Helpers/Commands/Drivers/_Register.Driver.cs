using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Responses.Drivers;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        public class _Register_Driver
        {
            public static void With(UserClaims persona, string organizationKey, string key, string name) =>
                Client.Use(persona).Drivers.Register(organizationKey, new CreateDriverModel {Name = name, Key = key});

            public static void With(UserClaims persona, string organizationKey, string key) =>
                With(persona, organizationKey, key, Guid.NewGuid().ToString());

            public static void With(UserClaims persona, string organizationKey) =>
                With(persona, organizationKey, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    }
}
