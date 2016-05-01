using System;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;

namespace Trackwane.Framework.Infrastructure.Requests.Metrics
{
    internal class MetricsHandler<T> : RequestHandler<T> where T : class, IRequest
    {
        private readonly IMetricsProvider metricsProvider;

        public MetricsHandler(IMetricsProvider metricsProvider, ILog log) : base(log)
        {
            this.metricsProvider = metricsProvider;
        }

        public override T Handle(T msg)
        {
            var start = DateTime.Now;

            T innerResult;

            try
            {
                innerResult = base.Handle(msg);
                metricsProvider.Requests.Success(typeof (T));
            }
            catch
            {
                metricsProvider.Requests.Failure(typeof (T));
                throw;
            }
            finally
            {
                metricsProvider.Requests.Duration(typeof(T), DateTime.Now.Subtract(start));
            }
     
            return innerResult;
        }
    }
}
