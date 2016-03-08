using Trackwane.Framework.Client;

namespace Trackwane.Framework.Integration
{
    internal class IntegrationContext : ContextClient<IntegrationContext>
    {
        public IntegrationContext(string baseUrl) : base(baseUrl)
        {
        }

        public void CheckFramework()
        {
            POST(client, Expand("check"));
        }
    }
}
