using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Boundaries
{
    public class ArchiveBoundary : UserCommand
    {
        public string BoundaryId { get; private set; }

        public ArchiveBoundary(string applicationKey, string requesterId, string organizationId, string boundaryId) : base(applicationKey, requesterId)
        {
            BoundaryId = boundaryId;
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; private set; }
    }
}
