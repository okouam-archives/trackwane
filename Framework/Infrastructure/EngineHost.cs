using System.Collections.Generic;
using System.Web.Http.SelfHost;
using NLog;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.messaginggateway.rmq;
using paramore.brighter.serviceactivator;
using StructureMap;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web;

namespace Trackwane.Framework.Infrastructure
{
    public class EngineHost<T> : IEngineHost where T : StructureMap.Registry, new()
    {
        private readonly IServiceLocator<T> locator;
        private HttpSelfHostServer web;
        private Dispatcher dispatcher;

        /* Public */

       public IExecutionEngine ExecutionEngine { get; set; }

        public IEngineHostConfig Configuration { get; set; }

        public EngineHost(IServiceLocator<T> locator, IEngineHostConfig config)
        {
            this.locator = locator;
            Configuration = config;
        }

        public void Start()
        {
            if (Configuration.Events == null)
            {
                logger.Warn("The engine will not listen for events given no event definitions have been provided");

                if (Configuration.Listeners == null)
                {
                    logger.Warn("The engine will not listen for events given no event listeners have been provided");
                }
            }

            if (Configuration.Handlers == null)
            {
                logger.Warn("The engine will not listen for commands given no handlers have been provided for the API");

                if (Configuration.ListenUri == null)
                {
                    logger.Warn("The engine will not listen for commands given no URI has been provided for the API");
                }
            }

            var container = locator.GetContainer();
            
            var mapperFactory = new MapperFactory(container)
                .WithCommands(Configuration.Commands)
                .WithEvents(Configuration.Events);

            container.Configure(x =>
            {
                var subscribers = locator.GetSubscribers(Configuration.Listeners, Configuration.Handlers, Configuration.Events, Configuration.Commands);
                var commandProcessor = CommandProcessorFactory.Build(subscribers, container, mapperFactory);

                x.For<IAmACommandProcessor>().Singleton().Use(commandProcessor);

                x.For<IExecutionEngine>().Use(new ExecutionEngine(container, commandProcessor)).Singleton();
            });

            container.AssertConfigurationIsValid();

            if (Configuration.Listeners != null && Configuration.Events != null)
            {
                StartDispatcher(container, mapperFactory);
            }

            if (Configuration.Handlers != null && Configuration.ListenUri != null)
            {
                StartWebApi(container);
            }

            ExecutionEngine = container.GetInstance<IExecutionEngine>();
        }

        public void Stop()
        {
            if (Configuration.Listeners != null && Configuration.Events != null)
            {
                StopDispatcher();
            }

            if (Configuration.Handlers != null && Configuration.ListenUri != null)
            {
                StopWebApi();
            }
        }
        
        /* Private */

        private void StartDispatcher(IContainer container, MapperFactory mapperFactory)
        {
            var commandProcessor = container.GetInstance<IAmACommandProcessor>();
            
            var inputChannelFactory = new InputChannelFactory(new RmqMessageConsumerFactory(), new RmqMessageProducerFactory());

            var connections = new List<Connection>();

            if (Configuration.Events != null)
            {
                connections.AddRange(ConnectionFactory.GetDomainEventConnections(inputChannelFactory, Configuration.Events));
            }

            if (Configuration.Commands != null)
            {
                connections.AddRange(ConnectionFactory.GetCommandConnections(inputChannelFactory, Configuration.Commands));
            }
       
            dispatcher = DispatchBuilder.With()
                    .CommandProcessor(commandProcessor)
                    .MessageMappers(mapperFactory.CreateMappers())
                    .ChannelFactory(inputChannelFactory)
                    .Connections(connections)
                    .Build();

            dispatcher.Receive();
        }

        private void StartWebApi(IContainer container)
        {
            web = WebApiBootstrapper.CreateServer(container, Configuration.ListenUri.OriginalString);
            web.OpenAsync().Wait();
        }

        private void StopDispatcher()
        {
            dispatcher.End().Wait();
            dispatcher = null;
        }

        private void StopWebApi()
        {
            web.CloseAsync().Wait();
            web = null;
        }

        private readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
