using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Alerts
{
    public class CreateAlert : UserCommand
    {
        public string AlertKey { get; set; }

        public string Name { get; }

        public string OrganizationKey { get; }

        public CreateAlert(string requesterId, string organizationKey, string name, string alertKey = null) : base(requesterId)
        {
            OrganizationKey = organizationKey;
            Name = name;
            AlertKey = alertKey;
        }
    }
}
