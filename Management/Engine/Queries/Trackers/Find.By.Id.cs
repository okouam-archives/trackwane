using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Responses.Trackers;

namespace Trackwane.Management.Queries.Trackers
{
    public class FindById : Query<TrackerDetails>, IScopedQuery
    {
        public TrackerDetails Execute(string trackerId)
        {
            return Execute(repository =>
            {
                var tracker = repository.Load<Tracker>(trackerId);

                if (tracker == null) return null;

                return new TrackerDetails
                {
                    Id = tracker.Key,
                    HardwareId = tracker.HardwareId,
                    IsArchived = tracker.IsArchived,
                    Model = tracker.Model
                };
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
