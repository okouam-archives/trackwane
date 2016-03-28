using System;
using Trackwane.AccessControl.Models;
using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Register_User
        {
            public static void With(UserClaims persona, string organizationKey, string userKey)
            {
                With(persona, organizationKey, userKey, $"{Faker.NameFaker.Name()} [{userKey}");
            }

            public static void With(UserClaims persona, string organizationKey, string userKey, string displayName)
            {
                With(persona, organizationKey, userKey, displayName, $"{Faker.InternetFaker.Email()} [{userKey}]",
                    Guid.NewGuid().ToString());
            }

            public static void With(UserClaims persona, string organizationKey, string userKey, string displayName,
                string email)
            {
                With(persona, organizationKey, userKey, displayName, email, Guid.NewGuid().ToString());
            }

            public static void With(UserClaims persona, string organizationKey, string userKey, string displayName, string email, string password)
            {
                Client.Use(persona).RegisterUser(organizationKey, new RegisterUserModel
                {
                    UserKey = userKey,
                    DisplayName = displayName,
                    Email = email,
                    Password = password
                });
            }
        }
    }
}
