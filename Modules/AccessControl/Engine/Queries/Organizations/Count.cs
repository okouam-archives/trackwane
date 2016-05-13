using System.Linq;
using Marten;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Organizations
{
    public class Count : Query<int>, IApplicationQuery
    {
        public Count(IDocumentStore documentStore) : base(documentStore)
        {

        }

        public int Execute()
        {
            return Execute(repository => repository.Query<Organization>().Count(x => x.ApplicationKey == ApplicationKey));
        }
    }
}
