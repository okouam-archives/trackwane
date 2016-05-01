using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _Create_Alert
        {
            public static void With(UserClaims claims, string organizationKey, string key, string name)
            {
                Client.Use(claims).Alerts.Create(organizationKey, name, new CreateAlertModel {Key = key, Name = name});
            }

            public static void With(UserClaims claims, string organizationKey, string key)
            {
                With(claims, organizationKey, key, Guid.NewGuid().ToString());
            }
        }
    }
}