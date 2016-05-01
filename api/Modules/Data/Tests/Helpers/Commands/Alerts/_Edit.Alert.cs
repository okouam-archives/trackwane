using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _Edit_Alert
        {
            public static void With(UserClaims claims, string organizationKey, string key, string name)
            {
                Client.Use(claims).Alerts.Update(organizationKey, key, new UpdateAlertModel {Name = name});
            }
        }
    }
}