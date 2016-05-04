using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using StructureMap;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Factories
{
    public class ServiceLocationFactory : IServiceLocationFactory
    {
        /* Public */

        public Container GetContainer(params StructureMap.Registry[] registries)
        {
            return new Container(x =>
            {
                foreach (var registry in registries)
                {
                    x.IncludeRegistry(registry);
                }
            });
        }

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

  
        /* Private */

        private void RegisterListeners(IEnumerable<Type> listeners, IEnumerable<Type> events)
        {
            if (listeners != null && events != null)
            {
                var listenerCollection = listeners.ToList();

                foreach (var candidate in listenerCollection)
                {
                    log.Debug(String.Format("Found the candidate listener <{0}> while registering listeners", candidate.Name));
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
                log.Warn("No assembly containing handler implementations was provided to register handlers");
                return;
            }

            if (commands == null)
            {
                log.Warn("No assembly containing commands was provided to register handlers");
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
                log.Debug(String.Format("Found the command <{0}> while registering handlers", command.Name));
            }
        }


        private static readonly ILog log = LogManager.GetLogger(typeof(ServiceLocationFactory));
    }
}
