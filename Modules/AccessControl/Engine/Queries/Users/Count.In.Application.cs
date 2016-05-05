using System.Linq;
using Marten;
using Trackwane.AccessControl.Contracts.Contracts;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class CountInApplication : Query<long>, IApplicationQuery
    {
        public CountInApplication(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public long Execute()
        {
            return Execute(repository =>
            {
                return repository.Query<User>().Count(x => x.ApplicationKey == ApplicationKey);
            });
        }
    }
}
