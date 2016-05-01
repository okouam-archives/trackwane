using System.Linq;
using Marten;
using Marten.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class Repository : IRepository
    {
        /* Public */

        public Repository(IDocumentSession session)
        {
            this.session = session;
        }

        public T Find<T>(string key, string organizationKey, string applicationKey) where T : ScopedAggregateRoot
        {
            return session.Query<T>().SingleOrDefault(x => x.Key == key && x.OrganizationKey == organizationKey && x.ApplicationKey == applicationKey);
        }

        public T Find<T>(string key, string applicationKey) where T : AggregateRoot
        {
            return session.Query<T>().SingleOrDefault(x => x.Key == key && x.ApplicationKey == applicationKey);
        }

        public IMartenQueryable<T> Query<T>() 
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
    }
}