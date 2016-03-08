using Raven.Client;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
            session = documentStore.OpenSession();
            session.Advanced.MaxNumberOfRequestsPerSession = 1000; // THIS IS WRONG! REMOVE THIS! NO!!!! 
        }

        public IRepository GetRepository()
        {
            return repository ?? (repository = new Repository(session, documentStore.DatabaseCommands));
        }

        public void Commit()
        {
            session.SaveChanges();
        }

        public void Dispose()
        {
            session.Dispose();
        }

        /* Private */

        private IRepository repository;
        private readonly IDocumentSession session;
        private readonly IDocumentStore documentStore;
    }
}