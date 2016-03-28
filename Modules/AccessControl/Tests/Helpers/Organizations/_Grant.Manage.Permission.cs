using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Grant_Manage_Permission
        {
            public static void With(UserClaims persona, string organizationKey, string userKey)
            {
                Client.Use(persona).GrantManagePermission(organizationKey, userKey);
            }
        }
    }
}
