using Trackwane.AccessControl.Models.Users;
using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Update_User
        {
            public static void With(UserClaims persona, string organizationKey, string userKey, string displayName = null, string email = null, string password = null)
            {
                Client.Use(persona).Users.UpdateUser(organizationKey, userKey, new UpdateUserModel
                {
                    DisplayName = displayName,
                    Email = email,
                    Password = password
                });
            }
        }
    }
}
