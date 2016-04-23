using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Revoke_Manage_Permission
        {
            public static void With(UserClaims persona, string organizationKey, string key)
            {
                Client.Use(persona).Organizations.RevokeManagePermission(organizationKey, key);
            }
        }
    }
}
