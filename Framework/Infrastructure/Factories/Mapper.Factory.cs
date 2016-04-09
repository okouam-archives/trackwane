using System;
using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor;
using Serilog;
using StructureMap;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Framework.Infrastructure.Factories
{
    public class MapperFactory : IAmAMessageMapperFactory
    {
        private readonly IContainer container;

        public MapperFactory(IContainer container)
        {
            this.container = container;
        }

        public MapperFactory WithEvents(IEnumerable<Type> someEvents)
        {
            events = someEvents;
            return this;
        }

        public MapperFactory WithCommands(IEnumerable<Type> someCommands)
        {
            commands = someCommands;
            return this;
        }

        public IAmAMessageMapper Create(Type messageMapperType)
        {
            return container.GetInstance(messageMapperType) as IAmAMessageMapper;
        }

        public MessageMapperRegistry CreateMappers()
        {
            var registry = new MessageMapperRegistry(this);

            if (commands != null)
            {
                foreach (var cmd in commands)
                {
                    var target = typeof (RequestMapper<>).MakeGenericType(cmd);
                    registry.Add(cmd, target);
                    Log.Debug(String.Format("Added a message mapper for <{0}>", cmd.Name));
                }
            }
            else
            {
                Log.Warning("No assembly was provided to register message mappers for commands");
            }

            if (events != null && events.Any())
            {
                foreach (var evt in events)
                {
                    var target = typeof (RequestMapper<>).MakeGenericType(evt);
                    registry.Add(evt, target);
                    Log.Debug(String.Format("Added a message mapper for <{0}>", evt.Name));
                }
            }
            else
            {
                Log.Warning("No assembly was provided to register message mappers for events");
            }

            return registry;
        }

        private IEnumerable<Type> commands;
        private IEnumerable<Type> events;
    }
}
