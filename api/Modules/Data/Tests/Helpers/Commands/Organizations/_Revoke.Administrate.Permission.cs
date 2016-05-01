using Trackwane.Framework.Common;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        public class _RevokeAdministratePermission
        {
            public static void With(UserClaims persona, string organizationKey, string key)
            {
                Client.Use(persona).Organizations.RevokeAdministratePermission(organizationKey, key);
            }
        }
    }
}
