using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Models.Boundaries;

namespace Trackwane.Management.Engine.Queries.Boundaries
{
    public class FindById : Query<BoundaryDetails>, IScopedQuery
    {
        public BoundaryDetails Execute(string boundaryId)
        {
            return Execute(repository =>
            {
                var boundary = repository.Load<Boundary>(boundaryId);

                if (boundary == null) return null;

                return new BoundaryDetails
                {
                    Name = boundary.Name,
                    IsArchived = boundary.IsArchived,
                    Coordinates = boundary.Coordinates,
                    Type = boundary.Type.ToString()
                };
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
