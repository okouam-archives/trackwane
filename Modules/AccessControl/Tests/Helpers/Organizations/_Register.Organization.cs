using System;
using Trackwane.AccessControl.Contracts.Models;
using Trackwane.Framework.Common;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Register_Organization
        {
            public static void With(UserClaims persona)
            {
                With(persona, GenerateKey());
            }

            public static void With(UserClaims persona, string organizationKey)
            {
                With(persona, organizationKey, String.Format(@"{0} [{1}]", Faker.CompanyFaker.Name(), organizationKey));
            }

            public static void With(UserClaims persona, string organizationKey, string name)
            {
                Client.Use(persona).Organizations.RegisterOrganization(new RegisterOrganizationModel
                {
                    OrganizationKey = organizationKey,
                    Name = name
                });
            }
        }
    }
}
