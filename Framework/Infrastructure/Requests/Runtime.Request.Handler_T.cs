using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests.Metrics;
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
            Publish(changes.ToArray());
        }

        protected void Publish(params DomainEvent[] changes)
        {
            if (changes != null && changes.Any())
            {
                foreach (var evt in changes)
                {
                    PublishEvent(evt);
                }
            }
        }
        
        private void PublishEvent(DomainEvent evt)
        {
            Logger.Debug(string.Format("Posting the event <{0}>: \r\n", evt.GetType().Name) + JsonConvert.SerializeObject(evt, Formatting.Indented));

            var executionEngineType = typeof(IAmACommandProcessor);

            var method =    executionEngineType .GetMethod("Post");

            var genericMethod = method.MakeGenericMethod(evt.GetType());

            genericMethod.Invoke(engine, new object[] {evt});
        }

        protected readonly IProvideTransactions transaction;
        protected readonly IExecutionEngine engine;
    }
}