using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Drivers
{
    public class RegisterDriver : UserCommand
    {
        public string DriverKey { get; set;  }

        public string OrganizationKey { get; }

        public string Name { get; }

        public RegisterDriver(string requesterId, string organizationKey, string name, string driverKey) : base(requesterId)
        {
            DriverKey = driverKey;
            OrganizationKey = organizationKey;
            Name = name;
        }
    }
}
