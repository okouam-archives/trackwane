using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Users
{
    public class RegisterUser : UserCommand
    {
        public string UserKey { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public string OrganizationKey { get; set; }

        public RegisterUser(string requesterKey, string organizationKey, string userKey, string displayName, string email, string password) : base(requesterKey)
        {
            OrganizationKey = organizationKey;
            DisplayName = displayName;
            Email = email;
            Password = password;
            UserKey = userKey;
            Role = Role.Standard;
        }
    }
}
