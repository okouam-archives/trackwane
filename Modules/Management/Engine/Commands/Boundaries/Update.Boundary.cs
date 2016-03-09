using Geo.Geometries;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Boundaries
{
    public class UpdateBoundary : UserCommand
    {
        /* Public */

        public string BoundaryId { get; }

        public Polygon Coordinates { get; set; }

        public string OrganizationId { get; }

        public string Name { get; set;  }

        public UpdateBoundary(string requesterId, string organizationId, string boundaryId) : base(requesterId)
        {
            BoundaryId = boundaryId;
            OrganizationId = organizationId;
        }
    }
}
