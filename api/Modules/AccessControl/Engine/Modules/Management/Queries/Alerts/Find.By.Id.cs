using Marten;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Alerts
{
    public class FindByKey : Query<AlertDetails>, IOrganizationQuery
    {
        public AlertDetails Execute(string alertId)
        {
            return Execute(repository =>
            {
                var alert = repository.Find<Alert>(alertId, ApplicationKey);

                if (alert == null) return null;

                return new AlertDetails {IsArchived =  alert.IsArchived, Name = alert.Name, Threshold = alert.Threshold, Type = alert.Type.ToString()};
            });
        }

        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
