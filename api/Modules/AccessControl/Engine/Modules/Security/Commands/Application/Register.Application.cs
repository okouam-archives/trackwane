using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Application
{
    public class RegisterApplication : ApplicationCommand
    {
        public string UserKey { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public RegisterApplication(string applicationKey) : base(applicationKey)
        {
        }
    }
}
