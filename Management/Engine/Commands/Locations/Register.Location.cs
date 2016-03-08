using Geo.Geometries;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Locations
{
    public class RegisterLocation : UserCommand
    {
        public string LocationId { get; set;  }

        public string Name { get; }

        public Point Coordinates { get; }

        public string OrganizationId { get; }

        public RegisterLocation(string requesterId, string organizationId, string name, Point coordinates, string locationId) : base(requesterId)
        {
            Name = name;
            LocationId = locationId;
            Coordinates = coordinates;
            OrganizationId = organizationId;
        }
    }
}
