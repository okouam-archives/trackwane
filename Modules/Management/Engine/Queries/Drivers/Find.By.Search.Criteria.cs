using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Drivers
{
    public class FindBySearchCriteria : Query<ResponsePage<DriverSummary>>, IScopedQuery
    {
        public ResponsePage<DriverSummary> Execute(string name = null)
        {
            return Execute(repository =>
            {
                var query = repository.Query<Driver>()
                  .Where(x => x.OrganizationKey == OrganizationKey);

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Name == name);
                }

                var drivers = query.ToList();

                return new ResponsePage<DriverSummary>
                {
                    Items = drivers.Select(x => new DriverSummary(x.Key)).ToList(),
                    Total = drivers.Count
                };
            });


        }

        public FindBySearchCriteria(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
