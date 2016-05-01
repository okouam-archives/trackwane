using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class GrantViewPermission : UserCommand
    {
        public string UserKey { get; set; }

        public string OrganizationKey { get; set; }

        public GrantViewPermission(string applicationKey, string requesterId, string organizationKey, string userKey) : base(applicationKey, requesterId)
        {
            UserKey = userKey;
            OrganizationKey = organizationKey;
        }
    }
}
