using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using MassTransit;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class RuntimeRequestHandler<T> where T : class
    {
        protected RuntimeRequestHandler(IProvideTransactions transaction, ILog log)
        {
            this.transaction = transaction;
            this.log = log;
        }

        public ILog log { get; set; }

        protected void Publish(ConsumeContext<T> ctx, IEnumerable<DomainEvent> changes)
        {
            Publish(ctx, changes.ToArray());
        }

        protected void Publish(ConsumeContext<T> ctx, params DomainEvent[] changes)
        {
            if (changes != null && changes.Any())
            {
                foreach (var evt in changes)
                {
                    ctx.Publish(evt);
                }
            }
        }
        
        public abstract Task Consume(ConsumeContext<T> ctx);

        protected readonly IProvideTransactions transaction;
    }
}