using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Engine.Commands.Organizations
{
    public class ArchiveOrganization : UserCommand
    {
        public string OrganizationKey { get; }

        public ArchiveOrganization(string requesterKey, string organizationKey) : base(requesterKey)
        {
            OrganizationKey = organizationKey;
        }
    }
}
