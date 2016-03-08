using System;
using paramore.brighter.commandprocessor;
using StructureMap;

namespace Trackwane.Framework.Infrastructure.Factories
{
    public class HandlerFactory : IAmAHandlerFactory
    {
        private readonly IContainer container;

        public HandlerFactory(IContainer container)
        {
            this.container = container;
        }

        public IHandleRequests Create(Type handlerType)
        {
            return container.GetInstance(handlerType) as IHandleRequests;
        }

        public void Release(IHandleRequests handler)
        {
            // do nothing
        }
    }
}