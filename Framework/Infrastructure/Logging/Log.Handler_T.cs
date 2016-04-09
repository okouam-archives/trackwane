using System;
using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;


namespace Trackwane.Framework.Infrastructure.Logging
{
    internal class MessageLoggingHandler<T> : RuntimeRequestHandler<T> where T : class, IRequest
    {
        public MessageLoggingHandler(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(transaction, engine, log)
        {
        }

        public override T Handle(T msg)
        {
            if (msg is SystemCommand)
            {
                Logger.Debug(string.Format("{0}{1}", String.Format("Received <{0}> command: \r\n", msg.GetType().Name), JsonConvert.SerializeObject(msg, Formatting.Indented)));
            }
            else if (msg is DomainEvent)
            {
                Logger.Debug(String.Format("Received <{0}> event: \r\n", msg.GetType().Name) + JsonConvert.SerializeObject(msg, Formatting.Indented));
            }

            var result = base.Handle(msg);

            engine.AcknowledgeProcessing(msg);

            return result;
        }
    }
}
