using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Configuration;

namespace Trackwane.Framework.Integration
{
    internal class IntegrationContext : ContextClient<IntegrationContext>
    {
        public IntegrationContext(string baseUrl) : base(baseUrl, new PlatformConfig())
        {
        }

        public void CheckFramework()
        {
            POST(Expand("check"));
        }
    }
}
