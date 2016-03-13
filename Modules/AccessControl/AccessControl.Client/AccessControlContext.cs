using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.AccessControl.Client
{
    public partial class AccessControlContext : ContextClient<AccessControlContext>
    {
        public OrganizationCommandsAndQueries Organizations => new OrganizationCommandsAndQueries(client);

        public UserCommandsAndQueries Users => new UserCommandsAndQueries(client);

        public AccessControlContext(string baseUrl, IPlatformConfig config) : base(baseUrl, config)
        {
            
        }
    }
}
