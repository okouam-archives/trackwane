using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        public class _GrantAdministratePermission
        {
            public static void With(UserClaims persona, string organizationKey, string userKey)
            {
                Client.Use(persona).Organizations.GrantAdministratePermission(organizationKey, userKey);
            }
        }
    }
}
