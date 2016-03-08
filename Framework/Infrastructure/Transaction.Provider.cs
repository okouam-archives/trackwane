using Raven.Client;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class TransactionProvider : IProvideTransactions
    {
        private readonly IDocumentStore documentStore;

        public TransactionProvider(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public IUnitOfWork Begin()
        {
            return new UnitOfWork(documentStore);
        }
    }
}
