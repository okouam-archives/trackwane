using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Boundaries
{
    public class ArchiveBoundary : UserCommand
    {
        public string BoundaryId { get; }

        public ArchiveBoundary(string requesterId, string organizationId, string boundaryId) : base(requesterId)
        {
            BoundaryId = boundaryId;
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}
