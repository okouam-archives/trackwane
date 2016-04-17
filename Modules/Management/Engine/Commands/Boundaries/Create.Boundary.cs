using Geo.Geometries;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Boundaries
{
    public class CreateBoundary : UserCommand
    {
        public BoundaryType Type { get; private set; }

        public string BoundaryId { get; set;  }

        public Polygon Coordinates { get; private set; }

        public string Name { get; private set; }

        public string OrganizationId { get; private set; }

        public CreateBoundary(string applicationKey, string requesterId, string organizationId, string name, Polygon coordinates, BoundaryType type, string boundaryId) : base(applicationKey, requesterId)
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
