using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Alerts
{
    public class UpdateAlert : UserCommand
    {
        public string AlertId { get; }

        public string OrganizationId { get; }

        public string Name { get; set; }

        public UpdateAlert(string requesterId, string organizationId, string alertId, string name = null) : base(requesterId)
        {
            OrganizationId = organizationId;
            AlertId = alertId;
            Name = name;
        }
    }
}
