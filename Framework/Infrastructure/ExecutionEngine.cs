using System;
using System.Threading;
using System.Threading.Tasks;
using paramore.brighter.commandprocessor;
using StructureMap;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Requests.Metrics;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class ExecutionEngine : IExecutionEngine
    {
        private readonly IContainer container;
        private readonly IAmACommandProcessor commandProcessor;
        private readonly IMetricsProvider metricsProvider;

        public event EventHandler<IRequest> MessagePublished;

        public event EventHandler<IRequest> MessagePosted;

        public event EventHandler<IRequest> MessageSent;

        public ExecutionEngine(IContainer container, IAmACommandProcessor commandProcessor, IMetricsProvider metricsProvider)
        {
            this.container = container;
            this.commandProcessor = commandProcessor;
            this.metricsProvider = metricsProvider;
        }
        
        public void Send<T>(T cmd) where T  : class, IRequest
        {
            OnMessageSent(cmd);
            commandProcessor.Send(cmd);
        }

        public void Post<T>(T msg) where T : class, IRequest
        {
            OnMessagePosted(msg);
            commandProcessor.Post(msg);
        }
        
        public void Publish<T>(T evt) where T : class, IRequest
        {
            try
            {
                commandProcessor.Publish(evt);
                OnMessagePublished(evt);
                metricsProvider.Events.Success(evt.GetType());
            }
            catch
            {
                metricsProvider.Events.Failure(evt.GetType());
                throw;
            }
        }

        public Task SendAsync<T>(T command, bool continueOnCapturedContext = false, CancellationToken? ct = null) where T : class, IRequest
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync<T>(T @event, bool continueOnCapturedContext = false, CancellationToken? ct = null) where T : class, IRequest
        {
            throw new NotImplementedException();
        }

        public Task PostAsync<T>(T request, bool continueOnCapturedContext = false, CancellationToken? ct = null) where T : class, IRequest
        {
            throw new NotImplementedException();
        }

        public T Query<T>(string applicationKey) where T : IApplicationQuery
        {
            var query = container.GetInstance<T>();
            query.ApplicationKey = applicationKey;
            return query;
        }

        public T Query<T>(string applicationKey, string organizationKey) where T : IOrganizationQuery
        {
            var query = container.GetInstance<T>();
            query.ApplicationKey = applicationKey;
            query.OrganizationKey = organizationKey;
            return query;
        }

        protected void OnMessagePosted(IRequest evt)
        {
            if (MessagePosted != null)
            {
                MessagePosted(this, evt);
            }
        }

        protected void OnMessagePublished(IRequest evt)
        {
            if (MessagePublished != null)
            {
                MessagePublished(this, evt);
            }
        }

        protected void OnMessageSent(IRequest evt)
        {
            if (MessageSent != null)
            {
                MessageSent(this, evt);
            }
        }
    }
}
