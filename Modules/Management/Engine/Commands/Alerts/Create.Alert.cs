using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Alerts
{
    public class CreateAlert : UserCommand
    {
        public string AlertKey { get; set; }

        public string Name { get; private set; }

        public string OrganizationKey { get; private set; }

        public CreateAlert(string applicationKey, string requesterId, string organizationKey, string name, string alertKey = null) : base(applicationKey, requesterId)
        {
            OrganizationKey = organizationKey;
            Name = name;
            AlertKey = alertKey;
        }
    }
}
