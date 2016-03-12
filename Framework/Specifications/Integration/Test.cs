using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Integration;
using NLog;
using NUnit.Framework;
using paramore.brighter.commandprocessor;
using Trackwane.Framework.Fixtures;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
using Trackwane.Framework.Tests.Fakes;

namespace Trackwane.Framework.Integration
{
    public class Test
    {
        [Test]
        public void Check_Framework_Functionality()
        {
            var messages = new List<Tuple<String, IRequest>>();

            logger.Info("===================== Starting London Engine =====================");

            var London = CreateEngineHostWithoutHandlers();
            London.Start();
            London.ExecutionEngine.MessageProcessed += (o, request) => messages.Add(new Tuple<string, IRequest>("LONDON", request));

            logger.Info("===================== Starting Paris Engine =====================");

            var Paris = CreateEngineHostWithoutListeners(new Uri("http://localhost:8092"));
            Paris.Start();
            Paris.ExecutionEngine.MessageProcessed += (o, request) => messages.Add(new Tuple<string, IRequest>("PARIS", request));

            logger.Info("===================== Registering Organization in London ========");

            var client = new IntegrationContext("http://localhost:8092");

            client.Use(Persona.SystemManager()).CheckFramework();

            while (messages.All(x => x.Item2.GetType() != typeof (FrameworkChecked)))
            {            
                Thread.Sleep(2000);
            }
        }

        private static EngineHost<IntegrationRegistry> CreateEngineHostWithoutListeners(Uri listenUri)
        {
            var locator = new ServiceLocator<IntegrationRegistry>(locationFactory);
            return new EngineHost<IntegrationRegistry>(locator, new EngineHostConfig(commands, events, handlers, null, listenUri));
        }

        private static EngineHost<IntegrationRegistry> CreateEngineHostWithoutHandlers()
        {
            var locator = new ServiceLocator<IntegrationRegistry>(locationFactory);
            return new EngineHost<IntegrationRegistry>(locator, new EngineHostConfig(null, events, null, listeners, null));
        }

        private static readonly ServiceLocationFactory locationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new DocumentStoreConfig()));
        private static readonly IEnumerable<Type> commands = typeof(CheckFramework).Assembly.GetCommands();
        private static readonly IEnumerable<Type> handlers = typeof(CheckFrameworkHandler).Assembly.GetHandlers();
        private static readonly IEnumerable<Type> events = typeof(FrameworkChecked).Assembly.GetDomainEvents();
        private static readonly IEnumerable<Type> listeners = typeof(FrameworkCheckedListener).Assembly.GetListeners();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}