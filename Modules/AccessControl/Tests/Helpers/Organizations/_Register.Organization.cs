using System;
using Trackwane.AccessControl.Models.Oganizations;
using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Register_Organization
        {
            public static void With(UserClaims persona) =>
                With(persona, Guid.NewGuid().ToString());

            public static void With(UserClaims persona, string organizationKey) =>
                With(persona, organizationKey, $"{Faker.CompanyFaker.Name()} [{organizationKey}]");

            public static void With(UserClaims persona, string organizationKey, string name) =>
                Client.Use(persona).Organizations.RegisterOrganization(new RegisterOrganizationModel
                {
                    OrganizationKey = organizationKey,
                    Name = name
                });
        }
    }
}
