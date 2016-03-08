﻿using System;
using System.Threading;
using System.Threading.Tasks;
using paramore.brighter.commandprocessor;
using StructureMap;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class ExecutionEngine : IExecutionEngine, IAmACommandProcessor
    {
        private readonly IContainer container;
        private readonly IAmACommandProcessor commandProcessor;
 
        public event EventHandler<IRequest> MessagePublished;

        public event EventHandler<IRequest> MessagePosted;

        public event EventHandler<IRequest> MessageProcessed;

        public event EventHandler<IRequest> MessageSent;

        public ExecutionEngine(IContainer container, IAmACommandProcessor commandProcessor)
        {
            this.container = container;
            this.commandProcessor = commandProcessor;
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
            OnMessagePublished(evt);
            commandProcessor.Publish(evt);
        }

        public void AcknowledgeProcessing<T>(T msg) where T : class, IRequest
        {
            OnMessageProcessed(msg);
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

        public T Query<T>() where T : IUnscopedQuery
        {
            return container.GetInstance<T>();
        }

        public T Query<T>(string organizationKey) where T : IScopedQuery
        {
            var query = container.GetInstance<T>();
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

        protected void OnMessageProcessed(IRequest evt)
        {
            if (MessageProcessed != null)
            {
                MessageProcessed(this, evt);
            }
        }
    }
}
