using System.Collections.Generic;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class RuntimeRequestHandler<T> : RequestHandler<T> where T : class, IRequest
    {
        protected RuntimeRequestHandler(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(log)
        {
            this.transaction = transaction;
            this.engine = engine;
        }

        protected void Publish(IEnumerable<DomainEvent> changes)
        {
            foreach (var evt in changes)
            {
                Publish(evt);
            }
        }

        protected void Publish(DomainEvent evt)
        {
            Logger.Debug($"Posting the event <{evt.GetType().Name}>: \r\n" + JsonConvert.SerializeObject(evt, Formatting.Indented));

            typeof(IExecutionEngine)
                .GetMethod("Post")
                .MakeGenericMethod(evt.GetType())
                .Invoke(engine, new object[] {evt});
        }

        protected readonly IProvideTransactions transaction;
        protected readonly IExecutionEngine engine;
    }
}