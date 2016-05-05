using FluentValidation.Attributes;
using Trackwane.AccessControl.Contracts.Validators;

namespace Trackwane.AccessControl.Contracts.Contracts
{
    [Validator(typeof(RegisterApplicationRequestValidator))]
    public class RegisterApplicationRequest
    {
        public RegisterApplicationRequest(string email, string displayName, string password, string secretKey)
        {
            Email = email;
            DisplayName = displayName;
            Password = password;
            SecretKey = secretKey;
        }

        public RegisterApplicationRequest()
        {
        }

        public string SecretKey { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}