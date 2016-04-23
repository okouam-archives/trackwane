using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Grant_View_Permission
        {
            public static void With(UserClaims persona, string organizationKey, string userKey)
            {
                Client.Use(persona).Organizations.GrantViewPermission(organizationKey, userKey);
            }
        }
    }
}
