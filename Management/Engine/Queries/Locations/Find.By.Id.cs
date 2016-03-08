using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Responses.Locations;

namespace Trackwane.Management.Queries.Locations
{
    public class FindById : Query<LocationDetails>, IScopedQuery
    {
        public LocationDetails Execute(string locationId)
        {
            return Execute(repository =>
            {
                var location = repository.Load<Location>(locationId);

                if (location == null) return null;

                return new LocationDetails
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
