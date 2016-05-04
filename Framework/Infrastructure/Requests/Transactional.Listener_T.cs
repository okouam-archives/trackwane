using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using MassTransit;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class TransactionalListener<T> : RuntimeRequestHandler<T> where T : class
    {
        protected TransactionalListener(
            IProvideTransactions transaction, 
            IExecutionEngine engine, 
            ILog log) 
            : base(transaction, log)
        {
        }

        public override Task Consume(ConsumeContext<T> ctx)
        {
            using (var uow = transaction.Begin())
            {
                var repository = uow.GetRepository();

                var events = Handle(ctx.Message, repository);

                uow.Commit();

                Publish(ctx, events);

                return Task.CompletedTask;
            }
        }

        /* Protected */

        protected abstract IEnumerable<DomainEvent> Handle(T cmd, IRepository repository);
    }
}
