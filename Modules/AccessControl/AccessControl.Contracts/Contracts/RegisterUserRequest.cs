namespace Trackwane.AccessControl.Engine.Controllers
{
    public class RegisterUserRequest
    {
        public RegisterUserRequest(string userKey, string email, string displayName, string password)
        {
            Email = email;
            DisplayName = displayName;
            Password = password;
            UserKey = userKey;
        }

        public RegisterUserRequest()
        {
        }

        public string UserKey { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}