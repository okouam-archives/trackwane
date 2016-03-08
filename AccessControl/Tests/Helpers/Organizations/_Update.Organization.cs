using Trackwane.AccessControl.Models.Oganizations;
using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Update_Organization
        {
            public static void With(UserClaims persona, string organizationKey, string name)
            {
                Client.Use(persona).Organizations.UpdateOrganization(organizationKey, new UpdateOrganizationModel
                {
                    Name = name
                });
            }
        }
    }
}
