using System;
using System.Collections.Generic;
using Prometheus;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Infrastructure.Requests.Metrics
{
    public class MetricsProvider : IMetricsProvider
    {
        public EventMetrics Events { get; set; }

        public RequestMetrics Requests { get; set; }

        public MetricsProvider(EventMetrics events, RequestMetrics requests)
        {
            Events = events;
            Requests = requests;
        }

        public MetricsProvider(string moduleName)
        {
            Events = new EventMetrics(moduleName);
            Requests = new RequestMetrics(moduleName);
        }
    }

    public class EventMetrics
    {
        private readonly string moduleName;
        private readonly Counter event_published_success_count;
        private readonly Counter event_published_failure_count;
        private const string event_published_failure_count_help = "Total number of events which failed to be published by a module engine";
        private const string event_published_success_count_help = "Total number of events successfully published by a module engine";

        public EventMetrics(string moduleName)
        {
            this.moduleName = moduleName.Replace("-", "_");
            event_published_failure_count = Prometheus.Metrics.CreateCounter("event_published_error_count", event_published_failure_count_help, "module", "event");
            event_published_success_count = Prometheus.Metrics.CreateCounter("event_published_success_count", event_published_success_count_help, "module", "event");
        }

        public void Failure(Type evt)
        {
            event_published_failure_count.Labels(moduleName, evt.Name).Inc();
        }

        public void Success(Type evt)
        {
            event_published_success_count.Labels(moduleName, evt.Name).Inc();
        }
    }

    public class RequestMetrics
    {
        private readonly Counter request_success_count;
        private readonly Counter request_failure_count;
        private readonly Summary request_duration;
        private readonly string moduleName;

        public RequestMetrics(string originalModuleName)
        {
            moduleName = originalModuleName.Replace("-", "_");
            request_duration = Prometheus.Metrics.CreateSummary("request_duration", "", "module", "type", "request");
            request_success_count = Prometheus.Metrics.CreateCounter("request_success_count", "", "module", "type", "request");
            request_failure_count = Prometheus.Metrics.CreateCounter("request_failure_count", "", "module", "type", "request");
        }

        public void Duration(Type msg, TimeSpan period)
        {
            request_duration.Labels(moduleName, "type", msg.Name).Observe(period.TotalMilliseconds);
        }

        public void Success(Type msg)
        {
            request_success_count.Labels(moduleName, "type", msg.Name).Inc();
        }

        public void Failure(Type msg)
        {
            request_failure_count.Labels(moduleName, "type", msg.Name).Inc();
        }
    }

    public interface IMetricsProvider
    {
        EventMetrics Events { get; }

        RequestMetrics Requests { get; }
    }
}
