using Trackwane.Framework.Client;

namespace Trackwane.AccessControl.Client
{
    public partial class AccessControlContext : ContextClient<AccessControlContext>
    {
        public OrganizationCommandsAndQueries Organizations => new OrganizationCommandsAndQueries(client);

        public UserCommandsAndQueries Users => new UserCommandsAndQueries(client);

        public AccessControlContext(string baseUrl) : base(baseUrl)
        {
            
        }
    }
}
