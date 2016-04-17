using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class ArchiveOrganization : UserCommand
    {
        public string OrganizationKey { get; private set; }

        public ArchiveOrganization(string applicationKey, string requesterKey, string organizationKey) : base(applicationKey, requesterKey)
        {
            OrganizationKey = organizationKey;
        }
    }
}
