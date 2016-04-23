using System;
using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Register_Location
        {
            public static void With(UserClaims persona, string organizationKey)
            {
                With(persona, organizationKey, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }

            public static void With(UserClaims persona, string organizationKey, string key)
            {
                With(persona, organizationKey, key, Guid.NewGuid().ToString(), new Point(0, 0));
            }

            public static void With(UserClaims persona, string organizationKey, string key, string name)
            {
                With(persona, organizationKey, key, name, new Point(0, 0));
            }

            public static void With(UserClaims persona, string organizationKey, string key, string name, Point coordinates)
            {
                Client.Use(persona).Locations.Register(organizationKey, new RegisterLocationModel
                {
                    Key = key,
                    Name = name,
                    Coordinates = coordinates != null ? new Geo.IO.GeoJson.GeoJsonWriter().Write(coordinates) : null
                });
            }
        }
    }
}
