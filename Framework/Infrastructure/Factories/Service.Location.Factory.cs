using System;
using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor;
using Raven.Client;
using Serilog;
using StructureMap;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Factories
{
    public class ServiceLocationFactory : IServiceLocationFactory
    {
        /* Public */

        public ServiceLocationFactory(IDocumentStoreBuilder builder)
        {
            this.builder = builder;
        }

        public Container GetContainer(params StructureMap.Registry[] registries) =>
            new Container(x =>
            {
                foreach (var registry in registries)
                {
                    x.IncludeRegistry(registry);
                }

                x.For<IDocumentStore>().Use(builder.CreateDocumentStore());
            });

        public IServiceLocationFactory WithListeners(IEnumerable<Type> listeners, IEnumerable<Type> events)
        {
            RegisterListeners(listeners, events);
            return this;
        }

        public IServiceLocationFactory WithHandlers(IEnumerable<Type> handlers, IEnumerable<Type> commands)
        {
            RegisterHandlers(handlers, commands);
            return this;
        }

        public SubscriberRegistry AsSubscriberRegistry() =>
            subscribers;
        
        /* Private */

        private void RegisterListeners(IEnumerable<Type> listeners, IEnumerable<Type> events)
        {
            if (listeners != null && events != null)
            {
                var listenerCollection = listeners.ToList();

                foreach (var candidate in listenerCollection)
                {
                    Log.Debug($"Found the candidate listener <{candidate.Name}> while registering listeners");
                }

                foreach (var evt in events)
                {
                    var target = typeof (RequestHandler<>).MakeGenericType(evt);

                    FindSubscribers(subscribers, listenerCollection, target, evt);
                }
            }
            else
            {
                throw new Exception("No listeners or events provided");
            }
        }

        private void RegisterHandlers(IEnumerable<Type> handlers, IEnumerable<Type> commands)
        {
            if (handlers == null)
            {
                Log.Warning("No assembly containing handler implementations was provided to register handlers");
                return;
            }

            if (commands == null)
            {
                Log.Warning("No assembly containing commands was provided to register handlers");
                return;
            }

            var commandCollection = commands.ToList();

            if (!commandCollection.Any())
            {
                throw new Exception("No commands provided");
            }

            var handlerCollection = handlers.ToList();

            if (!handlerCollection.Any())
            {
                throw new Exception("No command handlers provided");
            }

            foreach (var command in commandCollection)
            {
                Log.Debug($"Found the command <{command.Name}> while registering handlers");

                var target = typeof(RequestHandler<>).MakeGenericType(command);

                FindSubscribers(subscribers, handlerCollection, target, command);
            }
        }

        private static void FindSubscribers(SubscriberRegistry subscribers, IEnumerable<Type> candidates, Type target, Type request)
        {
            foreach (var candidate in candidates)
            {
                var current = candidate;

                while (true)
                {
                    if (current == typeof(object)) break;

                    var baseType = current.BaseType;

                    if (baseType == target)
                    {
                        Log.Debug($"Added <{candidate.Name}> for <{request.Name}>");
                        subscribers.Add(request, candidate);
                        break;
                    }

                    current = current.BaseType;
                }
            }
        }

        private readonly IDocumentStoreBuilder builder;
        private readonly SubscriberRegistry subscribers = new SubscriberRegistry();
    }
}
