using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Alerts
{
    public class UpdateAlert : UserCommand
    {
        public string AlertId { get; private set; }

        public string OrganizationId { get; private set; }

        public string Name { get; set; }

        public UpdateAlert(string requesterId, string organizationId, string alertId, string name = null) : base(requesterId)
        {
            OrganizationId = organizationId;
            AlertId = alertId;
            Name = name;
        }
    }
}
