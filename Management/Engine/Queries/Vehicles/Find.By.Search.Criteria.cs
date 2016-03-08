using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Responses.Vehicles;

namespace Trackwane.Management.Queries.Vehicles
{
    public class FindBySearchCriteria : Query<ResponsePage<VehicleSummary>>, IScopedQuery
    {
        public struct Criteria
        {
            public string Identifier { get; set; }
        }

        public ResponsePage<VehicleSummary> Execute(Criteria criteria)
        {
            return Execute(repository =>
            {
                var query = repository.Query<Vehicle>()
                   .Where(x => x.OrganizationKey == OrganizationKey);

                if (!string.IsNullOrEmpty(criteria.Identifier))
                {
                    query = query.Where(x => x.Identifier == criteria.Identifier);
                }

                var trackers = query.ToList();

                return new ResponsePage<VehicleSummary>
                {
                    Items = trackers.Select(x => new VehicleSummary()).ToList(),
                    Total = trackers.Count
                };
            });
        }

        public FindBySearchCriteria(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
