using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Responses.Alerts;

namespace Trackwane.Management.Queries.Alerts
{
    public class FindBySearchCriteria : Query<ResponsePage<AlertSummary>>, IScopedQuery
    {
        public ResponsePage<AlertSummary> Execute(string name = null)
        {
            return Execute(repository =>
            {
                var query = repository.Query<Alert>()
                  .Where(x => x.OrganizationKey == OrganizationKey);

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Name == name);
                }

                var alerts = query.ToList();
                
                return new ResponsePage<AlertSummary>
                {
                    Items = alerts.Select(x => new AlertSummary()).ToList(),
                    Total = alerts.Count
                };
            });
        }

        public FindBySearchCriteria(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
