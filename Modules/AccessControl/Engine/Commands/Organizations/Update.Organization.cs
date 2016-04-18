using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class UpdateOrganization : UserCommand
    {
        public string OrganizationKey { get; private set; }

        public string Name { get; set; }
        
        public UpdateOrganization(string applicationKey, string requesterKey, string organizationKey, string name) : base(applicationKey, requesterKey)
        {
            ApplicationKey = applicationKey;
            OrganizationKey = organizationKey;
            Name = name;
        }
    }
}
