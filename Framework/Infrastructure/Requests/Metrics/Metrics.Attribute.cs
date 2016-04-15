﻿using System;
using paramore.brighter.commandprocessor;

namespace Trackwane.Framework.Infrastructure.Requests.Metrics
{
    public class MetricsAttribute : RequestHandlerAttribute
    {
        public MetricsAttribute(int step, HandlerTiming timing) : base(step, timing)
        { }

        public override object[] InitializerParams()
        {
            return new object[] { Timing };
        }

        public override Type GetHandlerType()
        {
            return typeof(MetricsHandler<>);
        }
    }
}
