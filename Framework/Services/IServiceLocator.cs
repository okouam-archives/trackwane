using System;
using System.Collections.Generic;
using System.Reflection;
using paramore.brighter.commandprocessor;
using StructureMap;

namespace Trackwane.Framework.Interfaces
{
    public interface IServiceLocator<T> where T : Registry, new()
    {
        SubscriberRegistry GetSubscribers(Assembly engine, IEnumerable<Type> events);

        IContainer GetContainer();
    }
}
