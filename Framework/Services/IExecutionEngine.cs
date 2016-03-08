using System;
using paramore.brighter.commandprocessor;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Interfaces
{
    public interface IExecutionEngine
    {
        event EventHandler<IRequest> MessagePublished;

        event EventHandler<IRequest> MessageProcessed;

        event EventHandler<IRequest> MessageSent;

        event EventHandler<IRequest> MessagePosted;

        void Send<T>(T cmd) where T : class, IRequest;

        void Post<T>(T cmd) where T : class, IRequest;

        T Query<T>() where T : IUnscopedQuery;

        T Query<T>(string organizationKey) where T : IScopedQuery;

        void Publish<T>(T evt) where T : class, IRequest;

        void AcknowledgeProcessing<T>(T msg) where T : class, IRequest;
    }
}