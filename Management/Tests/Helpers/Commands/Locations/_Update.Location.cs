using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Management.Responses.Locations;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Update_Location
        {
            public static void With(UserClaims claims, string organizationId, string locationId, string name) =>
                With(claims, organizationId, locationId, name, null);

            public static void With(UserClaims claims, string organizationId, string locationId, string name, Point coordinates) =>
                Client.Use(claims).Locations.Update(organizationId, locationId, new UpdateLocationModel
                {
                    Name = name,
                    Coordinates = coordinates
                });
        }
    }
}
