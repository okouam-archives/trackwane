using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.SelfHost;
using log4net;
using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.messaginggateway.rmq;
using paramore.brighter.serviceactivator;
using StructureMap;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Web.DependencyResolution;
using Trackwane.Framework.Infrastructure.Web.Filters;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class EngineHost<T> : IEngineHost where T : StructureMap.Registry, new()
    {
        private readonly IServiceLocator<T> locator;
        private HttpSelfHostServer web;
        private Dispatcher dispatcher;
        private readonly ILog log = LogManager.GetLogger(typeof(EngineHost<T>));

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
                log.Warn("The engine will not listen for events given no event definitions have been provided");

                if (Configuration.Listeners == null)
                {
                    log.Warn("The engine will not listen for events given no event listeners have been provided");
                }
            }

            if (Configuration.Handlers == null)
            {
                log.Warn("The engine will not listen for commands given no handlers have been provided for the API");

                if (Configuration.ListenUri == null)
                {
                    log.Warn("The engine will not listen for commands given no URI has been provided for the API");
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

            if (Configuration.Handlers != null && Configuration.ListenUri != null)
            {
                log.Info("Starting the Web API");
                web = CreateStandaloneServer(container, Configuration.ListenUri.OriginalString);
                web.OpenAsync().Wait();
                log.Info("The Web API is start and waiting for connections");
            }


            if (Configuration.Listeners != null && Configuration.Events != null)
            {
                StartDispatcher(container, mapperFactory);
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
            log.Info("Starting the Command Dispatcher");

            var commandProcessor = container.GetInstance<IAmACommandProcessor>();
            
            var consumerFactory = new RmqMessageConsumerFactory();
            var producerFactory = new RmqMessageProducerFactory();
            var inputChannelFactory = new InputChannelFactory(consumerFactory, producerFactory);

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
            if (web != null)
            {
                web.CloseAsync().Wait();
                web = null;
            }
        }
        
        /* Private */

        private static HttpSelfHostServer CreateStandaloneServer(IContainer container, string url)
        {
            var configuration = new HttpSelfHostConfiguration(url);
            ApplyConfiguration(container, configuration);
            return new HttpSelfHostServer(configuration);
        }

        private static void ApplyConfiguration(IContainer container, HttpConfiguration configuration)
        {
            //configuration.EnableSwagger(c =>
            //{
            //    //c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
            //    c.SingleApiVersion("v1", "Trackwane API");
            //    c.UseFullTypeNameInSchemaIds();
            //}).EnableSwaggerUi();

            ResolveDependencies(configuration, container);
            RegisterRoutes(configuration);
        }
        
        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new BusinessRuleExceptionFilter());
            config.Filters.Add(new ValidationExceptionFilter());
        }

        private static void ResolveDependencies(HttpConfiguration configuration, IContainer container)
        {
            container.AssertConfigurationIsValid();
            configuration.DependencyResolver = new WebApiDependencyResolver(container, false);
        }
    }
}
