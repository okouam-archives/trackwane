using Marten;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.Framework.Tests.Fakes
{
    public class FakeQuery : Query<int>, IOrganizationQuery
    {
        public FakeQuery(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
