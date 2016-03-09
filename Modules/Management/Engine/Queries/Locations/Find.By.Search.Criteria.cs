using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Models.Locations;

namespace Trackwane.Management.Engine.Queries.Locations
{
    public class FindBySearchCriteria : Query<ResponsePage<LocationSummary>>, IScopedQuery
    {
        public ResponsePage<LocationSummary> Execute()
        {
            return Execute(repository =>
            {
                var locations = repository.Query<Location>()
                  .Where(x => x.OrganizationKey == OrganizationKey)
                  .ToList();

                return new ResponsePage<LocationSummary>
                {
                    Items = locations.Select(x => new LocationSummary()).ToList(),
                    Total = locations.Count
                };
            });
        }

        public FindBySearchCriteria(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
