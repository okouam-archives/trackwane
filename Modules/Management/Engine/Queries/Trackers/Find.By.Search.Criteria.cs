using System.Linq;
using Marten;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Trackers
{
    public class FindBySearchCriteria : Query<ResponsePage<TrackerSummary>>, IOrganizationQuery
    {
        public  ResponsePage<TrackerSummary> Execute(string model = null, string hardwareId = null)
        {
            return Execute(repository =>
            {
                var query = repository.Query<Tracker>()
                  .Where(x => x.OrganizationKey == OrganizationKey);

                if (!string.IsNullOrEmpty(hardwareId))
                {
                    query = query.Where(x => x.HardwareId == hardwareId);
                }

                if (!string.IsNullOrEmpty(model))
                {
                    query = query.Where(x => x.Model == model);
                }

                var trackers = query.ToList();

                return new ResponsePage<TrackerSummary>
                {
                    Items = trackers.Select(x => new TrackerSummary
                    {
                        Key = x.Key,
                        IsArchived = x.IsArchived,
                        HardwareId = x.HardwareId,
                        Model = x.Model
                    }).ToList(),
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
