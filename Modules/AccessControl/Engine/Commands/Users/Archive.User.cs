using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Users
{
    public class ArchiveUser : UserCommand
    {
        public string OrganizationKey { get; set; }

        public string UserKey { get; set; }

        public ArchiveUser(string requesterKey, string organizationKey, string userKey) : base(requesterKey)
        {
            OrganizationKey = organizationKey;
            UserKey = userKey;
        }
    }
}
