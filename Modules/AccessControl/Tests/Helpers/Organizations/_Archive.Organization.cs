using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Archive_Organization
        {
            public static void With(UserClaims persona, string organizationKey)
            {
                Client.Use(persona).ArchiveOrganization(organizationKey);
            }
        }
    }
}
