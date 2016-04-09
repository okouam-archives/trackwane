using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class UpdateOrganization : UserCommand
    {
        public string OrganizationKey { get; private set; }

        public string Name { get; set; }
        
        public UpdateOrganization(string requesterKey, string organizationKey, string name) : base(requesterKey)
        {
            OrganizationKey = organizationKey;
            Name = name;
        }
    }
}
