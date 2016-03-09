namespace Trackwane.AccessControl.Models.Users
{
    public class RegisterUserModel
    {
        public RegisterUserModel(string userKey, string email, string displayName, string password)
        {
            UserKey = userKey;
            Email = email;
            DisplayName = displayName;
            Password = password;
        }

        public RegisterUserModel()
        {
        }

        public string Password { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string UserKey { get; set; }
    }
}
