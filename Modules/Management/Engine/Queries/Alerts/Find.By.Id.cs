using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Alerts
{
    public class FindByKey : Query<AlertDetails>, IScopedQuery
    {
        public AlertDetails Execute(string alertId)
        {
            return Execute(repository =>
            {
                var alert = repository.Load<Alert>(alertId);

                if (alert == null) return null;

                return new AlertDetails(alert.IsArchived, alert.Name, alert.Threshold, alert.Type.ToString());
            });
        }

        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
