using Geo.Geometries;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Boundaries
{
    public class CreateBoundary : UserCommand
    {
        public BoundaryType Type { get; }

        public string BoundaryId { get; set;  }

        public Polygon Coordinates { get; }

        public string Name { get; }

        public string OrganizationId { get; }

        public CreateBoundary(string requesterId, string organizationId, string name, Polygon coordinates, BoundaryType type, string boundaryId) : base(requesterId)
        {
            BoundaryId = boundaryId;
            Coordinates = coordinates;
            Type = type;
            Name = name;
            OrganizationId = organizationId;
            RequesterId = requesterId;
        }
    }
}
