using System.Linq;
using Marten;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Vehicles
{
    public class FindBySearchCriteria : Query<ResponsePage<VehicleSummary>>, IOrganizationQuery
    {
        public ResponsePage<VehicleSummary> Execute(SearchVehiclesModel criteria)
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
                    Items = trackers.Select(x => new VehicleSummary {IsArchived = x.IsArchived, OrganizationId = x.OrganizationKey, Identifier = x.Identifier}).ToList(),
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
