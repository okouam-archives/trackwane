using Geo.Geometries;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Locations
{
    public class UpdateLocation : UserCommand
    {
        public string LocationId { get; private set; }

        public string Name { get; set; }

        public string OrganizationId { get; private set; }

        public Point Coordinates { get; set; }

        public UpdateLocation(string requesterId, string organizationId, string locationId) : base(requesterId)
        {
            LocationId = locationId;
            OrganizationId = organizationId;
        }
    }
}
