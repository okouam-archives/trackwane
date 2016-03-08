using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class RegisterOrganization : UserCommand
    {
        public string OrganizationKey { get; set; }

        public string Name { get; set; }

        public RegisterOrganization(string requesterKey, string organizationKey, string name) : base(requesterKey)
        {
            OrganizationKey = organizationKey;
            Name = name;
        }
    }
}
