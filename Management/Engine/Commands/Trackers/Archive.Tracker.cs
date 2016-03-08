using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Trackers
{
    public class ArchiveTracker : UserCommand
    {
        public string TrackerId { get; }

        public string OrganizationId { get; }

        public ArchiveTracker(string requesterId, string organizationId, string trackerId) : base(requesterId)
        {
            OrganizationId = organizationId;
            TrackerId = trackerId;
        }
    }
}
