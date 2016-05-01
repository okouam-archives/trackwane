using System.Linq;
using Marten;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Alerts
{
    public class FindBySearchCriteria : Query<ResponsePage<AlertSummary>>, IOrganizationQuery
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
                    Items = alerts.Select(x => new AlertSummary {Key = x.Key}).ToList(),
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
