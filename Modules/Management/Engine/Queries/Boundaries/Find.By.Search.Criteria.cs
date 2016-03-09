using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Models.Boundaries;

namespace Trackwane.Management.Engine.Queries.Boundaries
{
    public class FindBySearchCriteria : Query<ResponsePage<BoundarySummary>>, IScopedQuery
    {
        public ResponsePage<BoundarySummary> Execute(string name = null)
        {
            return Execute(repository =>
            {
                var query = repository.Query<Boundary>()
                    .Where(x => x.OrganizationKey == OrganizationKey);

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Name == name);
                }

                var boundaries = query.ToList();

                return new ResponsePage<BoundarySummary>
                {
                    Items = boundaries.Select(x => new BoundarySummary()).ToList(),
                    Total = boundaries.Count
                };
            });

        }

        public FindBySearchCriteria(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
