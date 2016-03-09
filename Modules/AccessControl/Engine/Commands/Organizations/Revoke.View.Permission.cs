using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class RevokeViewPermission : UserCommand
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }

        public RevokeViewPermission(string requesterKey, string organizationKey, string userKey) : base(requesterKey)
        {
            UserKey = userKey;
            OrganizationKey = organizationKey;
        }
    }
}
