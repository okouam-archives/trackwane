using Trackwane.AccessControl.Contracts.Models;

namespace Trackwane.AccessControl.Tests
{
    internal partial class Scenario
    {
        protected class Register_Application
        {
            public static void With(string secretKey, string userKey, string email, string password, string displayName)
            {
                Client.UseWithoutAuthentication().Application.Register(new RegisterApplicationModel
                {
                    UserKey = userKey,
                    Email = email,
                    DisplayName = displayName,
                    Password = password,
                    SecretKey = secretKey
                });
            }
        }
    }
}
