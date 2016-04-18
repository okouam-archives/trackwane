using System;
using Marten;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Queries
{
    public abstract class Query<TResult>
    {
        protected readonly IDocumentStore documentStore;

        public string ApplicationKey { get; set; }

        protected Query(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        protected TResult Execute(Func<IRepository, TResult> func)
        {
            using (var uow = new UnitOfWork(documentStore))
            {
                var repository = uow.GetRepository();
                return func(repository);
            }
        }
    }
}
