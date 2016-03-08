using System;
using System.Collections.Generic;
using paramore.brighter.commandprocessor;
using StructureMap;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class ServiceLocator<T> : IServiceLocator<T> where T : StructureMap.Registry, new()
    {
        private readonly IServiceLocationFactory serviceFactory;

        public ServiceLocator(IServiceLocationFactory serviceFactory)
        {
            if (serviceFactory == null) throw new ArgumentNullException(nameof(serviceFactory));

            this.serviceFactory = serviceFactory;
        }

        public SubscriberRegistry GetSubscribers(IEnumerable<Type> listeners, IEnumerable<Type> handlers, IEnumerable<Type> events, IEnumerable<Type> commands) =>
            serviceFactory
                .WithListeners(listeners, events)
                .WithHandlers(handlers, commands)
                .AsSubscriberRegistry();

        public IContainer GetContainer() =>
            serviceFactory.GetContainer(new T(), new Registry());
    }
}
