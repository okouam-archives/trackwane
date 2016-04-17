using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Users
{
    public class UpdateUser : UserCommand
    {
        public string DisplayName { get; set; }

        public string OrganizationKey { get; set; }

        public string UserKey { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UpdateUser(string applicationKey, string requesterKey, string organizationKey, string userKey) : base(applicationKey, requesterKey)
        {
            OrganizationKey = organizationKey;
            UserKey = userKey;
        }
    }
}
