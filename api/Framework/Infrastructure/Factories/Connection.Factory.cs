using System;
using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor;
using paramore.brighter.serviceactivator;

namespace Trackwane.Framework.Infrastructure.Factories
{
    internal class ConnectionFactory
    {
        public static IEnumerable<Connection> GetCommandConnections(IAmAChannelFactory inputChannelFactory, IEnumerable<Type> assembly)
        {
            return GetConnection(inputChannelFactory, assembly);
        }

        public static IEnumerable<Connection> GetDomainEventConnections(IAmAChannelFactory inputChannelFactory, IEnumerable<Type> assembly)
        {
            return GetConnection(inputChannelFactory, assembly);
        }

        private static IEnumerable<Connection> GetConnection(IAmAChannelFactory inputChannelFactory, IEnumerable<Type> commands)
        {
            return from command in commands
                   let connectionName = new ConnectionName(command.Name)
                   let channelName = new ChannelName(command.Name)
                   let routingKey = command.Name
                   select new Connection(connectionName, inputChannelFactory, command, channelName, routingKey, 1, 5000);
        }
    }
}
