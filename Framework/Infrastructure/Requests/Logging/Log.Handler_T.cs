using Newtonsoft.Json;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Infrastructure.Requests.Logging
{
    internal class MessageLoggingHandler<T> : RequestHandler<T> where T : class, IRequest
    {
        public MessageLoggingHandler(ILog log) : base(log)
        {
        }

        public override T Handle(T msg)
        {
            if (msg is SystemCommand)
            {
                Logger.Debug(string.Format("{0}{1}", string.Format("Received <{0}> command: \r\n", msg.GetType().Name), JsonConvert.SerializeObject(msg, Formatting.Indented)));
            }
            else if (msg is DomainEvent)
            {
                Logger.Debug(string.Format("Received <{0}> event: \r\n", msg.GetType().Name) + JsonConvert.SerializeObject(msg, Formatting.Indented));
            }

            return base.Handle(msg);
        }
    }
}

