using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Alerts
{
    public class ArchiveAlert : UserCommand
    {
        public string AlertId { get; private set; }

        public string OrganizationId { get; private set; }

        public ArchiveAlert(string applicationKey, string requesterId, string organizationId, string alertId) : base(applicationKey, requesterId)
        {
            AlertId = alertId;
            OrganizationId = organizationId;
        }
    }
}
