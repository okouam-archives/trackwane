using System;
using System.Collections.Generic;
using paramore.brighter.commandprocessor;
using StructureMap;

namespace Trackwane.Framework.Interfaces
{
    public interface IServiceLocationFactory
    {
        Container GetContainer(params Registry[] registries);

        IServiceLocationFactory WithListeners(IEnumerable<Type> listeners, IEnumerable<Type> events);

        IServiceLocationFactory WithHandlers(IEnumerable<Type> handlers, IEnumerable<Type> commands);

        SubscriberRegistry AsSubscriberRegistry();
    }
}