using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Alerts
{
    public class ArchiveAlert : UserCommand
    {
        public string AlertId { get; }

        public string OrganizationId { get; }

        public ArchiveAlert(string requesterId, string organizationId, string alertId) : base(requesterId)
        {
            AlertId = alertId;
            OrganizationId = organizationId;
        }
    }
}
