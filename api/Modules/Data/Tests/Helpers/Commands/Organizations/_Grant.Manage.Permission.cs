using Trackwane.Framework.Common;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class Grant_Manage_Permission
        {
            public static void With(UserClaims persona, string organizationKey, string userKey)
            {
                Client.Use(persona).Organizations.GrantManagePermission(organizationKey, userKey);
            }
        }
    }
}
