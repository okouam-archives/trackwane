using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class _Archive_User
        {
            public static void With(UserClaims persona, string organizationKey, string userKey)
            {
                Client.Use(persona).Users.ArchiveUser(organizationKey, userKey);
            }
        }
    }
}
