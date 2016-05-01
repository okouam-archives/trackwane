using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Marten;
using Microsoft.Owin.Hosting;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.messaginggateway.rmq;
using paramore.brighter.serviceactivator;
using Prometheus;
using StructureMap;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Requests.Metrics;
using Trackwane.Framework.Interfaces;
using ConnectionFactory = Trackwane.Framework.Infrastructure.Factories.ConnectionFactory;

namespace Trackwane.Framework.Infrastructure
{
    public class EngineHost<T> : IEngineHost where T : StructureMap.Registry, new()
    {
        private readonly string server;
        private readonly Assembly engine;
        private readonly Type[] events;
        private readonly IServiceLocator<T> locator;
        private IDisposable webApi;
        private MetricServer metricsCollection;
        private Dispatcher dispatcher;
        private readonly ILog log = LogManager.GetLogger(typeof(EngineHost<T>));

        /* Public */

        public IExecutionEngine ExecutionEngine { get; set; }

        public IModuleConfig Configuration { get; set; }

        public EngineHost(string server, IModuleConfig moduleConfig, Assembly engine, params Type[] events)
        {
            this.server = server;
            this.engine = engine;
            this.events = events;
            locator = new ServiceLocator<T>(new ServiceLocationFactory()); 
            Configuration = moduleConfig;
        }

        public void Start()
        {
            if (!events.Any())
            {
                log.Warn("The engine will not listen for events given no event definitions have been provided");

                if (!engine.GetListeners().Any())
                {
                    log.Warn("The engine will not listen for events given no event listeners have been provided");
                }
            }

            if (!engine.GetHandlers().Any())
            {
                log.Warn("The engine will not listen for commands given no handlers have been provided for the API");

                if (Configuration.Get("uri") == null)
                {
                    log.Warn("The engine will not listen for commands given no URI has been provided for the API");
                }
            }

            var container = locator.GetContainer();
            
            var mapperFactory = new MapperFactory(container)
                .WithCommands(engine.GetCommands())
                .WithEvents(events);

            container.Configure(x =>
            {
                var subscribers = locator.GetSubscribers(engine, events);

                var metricsProvider = new MetricsProvider(Configuration.ModuleName);

                x.For<IMetricsProvider>().Singleton().Use(metricsProvider);

                var documentStore = DocumentStore.For(Configuration.ConnectionString);

                x.For<IDocumentStore>().Use(documentStore);

                var commandProcessor = CommandProcessorFactory.Build(documentStore,  metricsProvider, subscribers, container, mapperFactory);
   
                x.For<IAmACommandProcessor>().Singleton().Use(commandProcessor);

                x.For<IExecutionEngine>().Singleton().Use(commandProcessor);
            });

            container.AssertConfigurationIsValid();

            StartWebApi(container);
            
            if (engine.GetListeners() != null && events != null)
            {
                StartDispatcher(container, mapperFactory);
            }

            StartMetricsCollection();

            ExecutionEngine = container.GetInstance<IExecutionEngine>();
        }
        
        public void Stop()
        {
            if (engine.GetListeners() != null && events != null)
            {
                StopDispatcher();
            }

            if (engine.GetHandlers() != null && Configuration.Get("protocol") != null && Configuration.Get("api-port") != null)
            {
                StopWebApi();
            }

            StopMetricsCollection();
        }
        
        /* Private */

        private void StartDispatcher(IContainer container, MapperFactory mapperFactory)
        {
            log.Info("Starting the Command Dispatcher");

            var commandProcessor = container.GetInstance<IAmACommandProcessor>();
            
            var consumerFactory = new RmqMessageConsumerFactory();
            var producerFactory = new RmqMessageProducerFactory();
            var inputChannelFactory = new InputChannelFactory(consumerFactory, producerFactory);

            var connections = new List<Connection>();

            if (events.Any())
            {
                connections.AddRange(ConnectionFactory.GetDomainEventConnections(inputChannelFactory, events));
            }

            if (engine.GetCommands().Any())
            {
                connections.AddRange(ConnectionFactory.GetCommandConnections(inputChannelFactory, engine.GetCommands()));
            }
       
            dispatcher = DispatchBuilder.With()
                    .CommandProcessor(commandProcessor)
                    .MessageMappers(mapperFactory.CreateMappers())
                    .ChannelFactory(inputChannelFactory)
                    .Connections(connections)
                    .Build();

            dispatcher.Receive();

            log.Info("The Command Dispatcher has been started");
        }
        
        private void StopDispatcher()
        {
            if (dispatcher != null)
            {
                dispatcher.End().Wait();
                dispatcher = null;
            }
        }

        private void StopWebApi()
        {
            if (webApi != null)
            {
                webApi.Dispose();
                webApi = null;
            }
        }

        private void StartMetricsCollection()
        {
            metricsCollection = new MetricServer("*", int.Parse(Configuration.Get("metrics-port")));
            metricsCollection.Start();
        }

        private void StopMetricsCollection()
        {
            metricsCollection.Stop();
        }

        private void StartWebApi(IContainer container)
        {
            var protocol = Configuration.Get("protocol");
            var port = Configuration.Get("api-port");

            if (engine.GetHandlers().Any() && protocol != null && port != null)
            {
                log.Info("Starting the Web API");
                var uri = protocol + "://" + server + ":" + port;
                webApi = CreateStandaloneServer(container, uri);
                log.Info("The Web API is start and waiting for connections");
            }
        }

        private static IDisposable CreateStandaloneServer(IContainer container, string url)
        {
            Startup.Container = container;
            return WebApp.Start<Startup>(url);
        }
    }
}
