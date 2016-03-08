using System.Linq;
using Raven.Client;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Models.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class FindByCredentials : Query<UserDetails>, IUnscopedQuery
    {
        public FindByCredentials(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public UserDetails Execute(string email, string password)
        {
            return Execute(repository =>
            {
                var user = repository.Query<User>().Customize(x => x.WaitForNonStaleResults()) .SingleOrDefault(x => x.Email == email);

                if (user != null && user.Credentials.IsValid(password))
                {
                    return new FindByKey(documentStore).Execute(user.Key);
                }

                return null;
            });
        }
    }
}
