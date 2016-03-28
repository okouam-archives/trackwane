using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Trackers
{
    public class FindById : Query<TrackerDetails>, IScopedQuery
    {
        public TrackerDetails Execute(string trackerId)
        {
            return Execute(repository =>
            {
                var tracker = repository.Load<Tracker>(trackerId);

                if (tracker == null) return null;

                return new TrackerDetails(tracker.Model, tracker.IsArchived, tracker.HardwareId, tracker.Key);
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
