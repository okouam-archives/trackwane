using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class RevokeViewPermission : UserCommand
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }

        public RevokeViewPermission(string applicationKey, string requesterKey, string organizationKey, string userKey) : base(applicationKey, requesterKey)
        {
            ApplicationKey = applicationKey;
            UserKey = userKey;
            OrganizationKey = organizationKey;
        }
    }
}
