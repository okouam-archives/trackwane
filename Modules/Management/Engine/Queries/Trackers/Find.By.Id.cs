using Marten;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Trackers
{
    public class FindById : Query<TrackerDetails>, IOrganizationQuery
    {
        public TrackerDetails Execute(string trackerId)
        {
            return Execute(repository =>
            {
                var tracker = repository.Find<Tracker>(trackerId, ApplicationKey);

                return tracker == null ? null : new TrackerDetails
                {
                    Model = tracker.Model,
                    IsArchived = tracker.IsArchived,
                    HardwareId = tracker.HardwareId,
                    Id = tracker.Key
                };
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
