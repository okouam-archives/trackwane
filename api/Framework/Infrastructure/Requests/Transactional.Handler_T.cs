using System.Collections.Generic;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests.Logging;
using Trackwane.Framework.Infrastructure.Requests.Metrics;
using Trackwane.Framework.Infrastructure.Validation;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class TransactionalHandler<T> : RuntimeRequestHandler<T> where T : class, IRequest
    {
        protected TransactionalHandler(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(transaction, engine, log)
        {
        }

        [Metrics(1, HandlerTiming.Before)]
        [Log(2, HandlerTiming.Before)]
        [Validate(3, HandlerTiming.Before)]
        public override T Handle(T cmd)
        {
            using (var uow = transaction.Begin())
            {
                var repository = uow.GetRepository();

                var events = Handle(cmd, repository);

                uow.Commit();

                Publish(events);
            }

            return base.Handle(cmd);
        }

        /* Protected */

        protected abstract IEnumerable<DomainEvent> Handle(T cmd, IRepository repository);
    }
}
