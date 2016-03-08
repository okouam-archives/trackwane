using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Locations
{
    public class ArchiveLocation: UserCommand
    {
        public string LocationId { get; }

        public ArchiveLocation(string requesterId, string organizationId, string locationId) : base(requesterId)
        {
            LocationId = locationId;
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}
