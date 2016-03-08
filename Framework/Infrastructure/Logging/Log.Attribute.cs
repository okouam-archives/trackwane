using System;
using paramore.brighter.commandprocessor;

namespace Trackwane.Framework.Infrastructure.Logging
{
    public class Log : RequestHandlerAttribute
    {
        public Log(int step, HandlerTiming timing) : base(step, timing)
        { }

        public override object[] InitializerParams()
        {
            return new object[] { Timing };
        }

        public override Type GetHandlerType()
        {
            return typeof(MessageLoggingHandler<>);
        }
    }
}
