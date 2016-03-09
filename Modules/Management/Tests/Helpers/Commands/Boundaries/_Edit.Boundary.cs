using System;
using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Management.Models.Boundaries;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Edit_Boundary
        {
            public static void With(UserClaims claims, string organizationKey, string key)
            {
                With(claims, organizationKey, key, Guid.Empty.ToString());
            }

            public static void With(UserClaims claims, string organizationKey, string key, string name)
            {
                With(claims, organizationKey, key, name, new Polygon());
            }

            public static void With(UserClaims claims, string organizationKey, string key, string name, Polygon coordinates)
            {
                Client.Use(claims).Boundaries.Update(organizationKey, key, new UpdateBoundaryModel
                {
                    Coordinates = coordinates,
                    Name = name
                });
            }
        }
    }
}
