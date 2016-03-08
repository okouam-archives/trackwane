using System;
using paramore.brighter.commandprocessor;

namespace Trackwane.Framework.Infrastructure.Validation
{
    public class ValidateAttribute : RequestHandlerAttribute
    {
        public ValidateAttribute(int step, HandlerTiming timing) : base(step, timing)
        { }

        public override object[] InitializerParams()
        {
            return new object[] { Timing };
        }

        public override Type GetHandlerType()
        {
            return typeof(ValidateHandler<>);
        }
    }
}
