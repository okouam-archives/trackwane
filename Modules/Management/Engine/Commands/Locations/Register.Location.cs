using Geo.Geometries;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Locations
{
    public class RegisterLocation : UserCommand
    {
        public string LocationId { get; set;  }

        public string Name { get; private set; }

        public Point Coordinates { get; private set; }

        public string OrganizationId { get; private set; }

        public RegisterLocation(string requesterId, string organizationId, string name, Point coordinates, string locationId) : base(requesterId)
        {
            Name = name;
            LocationId = locationId;
            Coordinates = coordinates;
            OrganizationId = organizationId;
        }
    }
}
