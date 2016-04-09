using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Alerts
{
    public class CreateAlert : UserCommand
    {
        public string AlertKey { get; set; }

        public string Name { get; private set; }

        public string OrganizationKey { get; private set; }

        public CreateAlert(string requesterId, string organizationKey, string name, string alertKey = null) : base(requesterId)
        {
            OrganizationKey = organizationKey;
            Name = name;
            AlertKey = alertKey;
        }
    }
}
