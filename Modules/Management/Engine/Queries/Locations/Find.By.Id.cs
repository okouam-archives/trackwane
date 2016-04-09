using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Locations
{
    public class FindById : Query<LocationDetails>, IScopedQuery
    {
        public LocationDetails Execute(string locationId)
        {
            return Execute(repository =>
            {
                var location = repository.Load<Location>(locationId);

                return location == null ? null : new LocationDetails
                {
                    Name = location.Name,
                    IsArchived = location.IsArchived,
                    Coordinates = location.Coordinates
                };
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
