using System.Linq;
using Marten;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class CountInSystem : Query<int>, IApplicationQuery
    {
        public CountInSystem(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public int Execute(string organizationKey = null)
        {
            return Execute(repository => repository.Query<User>().Count());
        }
    }
}
