using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Models.Trackers;

namespace Trackwane.Management.Engine.Queries.Trackers
{
    public class FindBySearchCriteria : Query<ResponsePage<TrackerSummary>>, IScopedQuery
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
                    Items = trackers.Select(x => new TrackerSummary()).ToList(),
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
