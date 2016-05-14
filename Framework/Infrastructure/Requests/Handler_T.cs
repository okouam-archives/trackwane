using System.Collections.Generic;
using System.Linq;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class Handler<T>
    {
        protected Handler(IExecutionEngine engine, IProvideTransactions transaction, ILog log)
        {
            this.engine = engine;
            this.transaction = transaction;
            this.log = log;
        }

        public IList<dynamic> Handle(T cmd)
        {
            using (var uow = transaction.Begin())
            {
                var repository = uow.GetRepository();

                var events = Handle(cmd, repository);

                uow.Commit();

                if (events != null)
                {
                    var all = events.Cast<dynamic>().ToList();

                    if (all.Any())
                    {
                        foreach (var evt in all)
                        {
                            log.Info($"Publishing event of type {evt.GetType()}");
                            engine.Publish(evt);
                        }
                    }

                    return all;
                }

                return new List<dynamic>();
            }
        }

        /* Protected */

        protected abstract IEnumerable<DomainEvent> Handle(T cmd, IRepository repository);

        private readonly IExecutionEngine engine;
        protected readonly IProvideTransactions transaction;
        protected ILog log;
    }
}
