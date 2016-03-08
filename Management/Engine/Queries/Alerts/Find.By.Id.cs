using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Responses.Alerts;

namespace Trackwane.Management.Queries.Alerts
{
    public class FindByKey : Query<AlertDetails>, IScopedQuery
    {
        public AlertDetails Execute(string alertId)
        {
            return Execute(repository =>
            {
                var alert = repository.Load<Alert>(alertId);

                if (alert == null) return null;

                return new AlertDetails
                {
                    Name = alert.Name,
                    IsArchived = alert.IsArchived,
                    Threshold = alert.Threshold,
                    Type = alert.Type.ToString()
                };
            });
        }

        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
