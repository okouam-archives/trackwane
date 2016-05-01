using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class RegisterOrganization : UserCommand
    {
        public string OrganizationKey { get; set; }

        public string Name { get; set; }

        public RegisterOrganization(string applicationKey, string requesterKey, string organizationKey, string name) : base(applicationKey, requesterKey)
        {
            ApplicationKey = applicationKey;
            OrganizationKey = organizationKey;
            Name = name;
        }
    }
}
