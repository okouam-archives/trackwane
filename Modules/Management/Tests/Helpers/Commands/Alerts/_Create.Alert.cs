using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Create_Alert
        {
            public static void With(UserClaims claims, string organizationKey, string key, string name)
            {
                Client.Use(claims).CreateAlert(organizationKey, name, new CreateAlertModel(key, name));
            }

            public static void With(UserClaims claims, string organizationKey, string key)
            {
                With(claims, organizationKey, key, Guid.NewGuid().ToString());
            }
        }
    }
}