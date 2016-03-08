using System;
using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Management.Commands.Boundaries;
using Trackwane.Management.Responses.Boundaries;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Create_Boundary
        {
            public static void With(UserClaims claims, string key, string name, string organizationKey, Polygon coordinates) =>
                Client.Use(claims).Boundaries.Create(organizationKey, new CreateBoundaryModel
                {
                    Coordinates = coordinates,
                    Name = name,
                    Key = key,
                    Type = BoundaryType.ExclusionZone.ToString()
                });

            public static void With(UserClaims claims, string organizationKey, string key, string name) =>
                With(claims, key, name, organizationKey, new Polygon());

            public static void With(UserClaims claims, string organizationKey, string key) =>
                With(claims, key, Guid.NewGuid().ToString(), organizationKey, new Polygon());

            public static void With(UserClaims claims, string organizationKey) =>
                With(claims, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), organizationKey, new Polygon());
        }
    }
}
