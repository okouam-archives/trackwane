using System.Linq;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Connection;
using Raven.Client.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class Repository : IRepository
    {
        /* Public */

        public Repository(IDocumentSession session, IDatabaseCommands databaseCommands)
        {
            this.session = session;
            this.databaseCommands = databaseCommands;
        }

        public T Load<T>(string key, string organizationKey) where T : ScopedAggregateRoot
        {
            return session.Query<T>().Customize(x => x.WaitForNonStaleResults()).SingleOrDefault(x => x.Key == key && x.OrganizationKey == organizationKey);
        }

        public T Load<T>(string key) where T : AggregateRoot
        {
            return session.Query<T>().Customize(x => x.WaitForNonStaleResults()).SingleOrDefault(x => x.Key == key);
        }

        public JsonDocument Get(string id)
        {
            return databaseCommands.Get(id);
        }

        public IRavenQueryable<T> Query<T>() 
        {
            return session.Query<T>();
        }

        public void Persist(object aggregateRoot)
        {
            session.Store(aggregateRoot);
        }

        public void Delete(object aggregateRoot)
        {
            session.Delete(aggregateRoot);
        }

        /* Private */

        private readonly IDocumentSession session;
        private readonly IDatabaseCommands databaseCommands;
    }
}