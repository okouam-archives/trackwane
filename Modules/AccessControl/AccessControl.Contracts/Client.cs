using System.Dynamic;
using Trackwane.AccessControl.Contracts.Scopes;
using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.AccessControl.Contracts
{
    public class AccessControlClient : ContextClient<AccessControlClient>
    {
        private UserScope users;
        private OrganizationScope organizations;
        private ApplicationScope application;
        private MetricScope metrics;

        public ApplicationScope Application
        {
            get { return application ?? (application = new ApplicationScope(this)); }
        }

        public UserScope Users
        {
            get { return users ?? (users = new UserScope(this)); }
        }
        
        public OrganizationScope Organizations
        {
            get { return organizations ?? (organizations = new OrganizationScope(this)); }
        }

        public MetricScope Metrics
        {
            get
            {
                return metrics ?? (metrics = new MetricScope(this));
            }
        }

        public AccessControlClient(string server, string protocol, string apiPort, string secretKey, string metricsPort, string applicationKey) 
            : base(server, protocol, apiPort, secretKey, metricsPort, applicationKey)
        {

        }
    }
}
