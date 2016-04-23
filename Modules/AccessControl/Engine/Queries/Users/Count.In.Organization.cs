using System.Linq;
using Marten;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class CountInOrganization : Query<int>, IOrganizationQuery
    {
        public CountInOrganization(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public int Execute()
        {
            return Execute(repository => repository.Query<User>().Count(x => x.ParentOrganizationKey == OrganizationKey));
        }

        public string OrganizationKey { get; set; }
    }
}
