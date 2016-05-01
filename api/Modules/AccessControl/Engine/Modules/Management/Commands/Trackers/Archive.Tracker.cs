using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Trackers
{
    public class ArchiveTracker : UserCommand
    {
        public string TrackerId { get; private set; }

        public string OrganizationId { get; private set; }

        public ArchiveTracker(string applicationKey, string requesterId, string organizationId, string trackerId) : base(applicationKey, requesterId)
        {
            OrganizationId = organizationId;
            TrackerId = trackerId;
        }
    }
}
