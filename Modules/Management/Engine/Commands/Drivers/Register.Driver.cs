using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Drivers
{
    public class RegisterDriver : UserCommand
    {
        public string DriverKey { get; set;  }

        public string OrganizationKey { get; private set; }

        public string Name { get; private set; }

        public RegisterDriver(string applicationKey, string requesterId, string organizationKey, string name, string driverKey) : base(applicationKey, requesterId)
        {
            DriverKey = driverKey;
            OrganizationKey = organizationKey;
            Name = name;
        }
    }
}
