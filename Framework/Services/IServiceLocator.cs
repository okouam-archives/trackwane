using System;
using System.Collections.Generic;
using paramore.brighter.commandprocessor;
using StructureMap;

namespace Trackwane.Framework.Interfaces
{
    public interface IServiceLocator<T> where T : Registry, new()
    {
        SubscriberRegistry GetSubscribers(IEnumerable<Type> listeners, IEnumerable<Type> handlers, IEnumerable<Type> events, IEnumerable<Type> commands);

        IContainer GetContainer();
    }
}
