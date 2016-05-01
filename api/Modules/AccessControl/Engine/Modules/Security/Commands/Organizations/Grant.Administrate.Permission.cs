using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class GrantAdministratePermission : UserCommand
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }

        public GrantAdministratePermission(string applicationKey, string requesterKey, string organizationKey, string userKey) : base(applicationKey, requesterKey)
        {
            UserKey = userKey;
            OrganizationKey = organizationKey;
        }
    }
}
