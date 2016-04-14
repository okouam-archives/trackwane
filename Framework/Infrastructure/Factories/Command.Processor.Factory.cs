using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using paramore.brighter.commandprocessor.messaginggateway.rmq;
using StructureMap;
using Trackwane.Framework.Infrastructure.Requests.Metrics;
using Trackwane.Framework.Infrastructure.Storage;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Factories
{
    public class CommandProcessorFactory
    {
        public static IExecutionEngine Build(IMetricsProvider metricsProvider, SubscriberRegistry subscribers, IContainer container, MapperFactory mapperFactory)
        {
            var logger = LogProvider.GetCurrentClassLogger();

            var messageStore = container.GetInstance<DocumentMessageStore>();

            var pipeline = CommandProcessorBuilder
                .With()
                .Handlers(new HandlerConfiguration(subscribers, new HandlerFactory(container)))
                .DefaultPolicy();

            var mappers = mapperFactory.CreateMappers();
            var messageProducer = new RmqMessageProducer(logger);
            var messagingConfiguration = new MessagingConfiguration(messageStore, messageProducer, mappers);
            var context = pipeline.TaskQueues(messagingConfiguration);
    
            var ctxFactory = new InMemoryRequestContextFactory();

            return new ExecutionEngine(container, context.RequestContextFactory(ctxFactory).Build(), metricsProvider);
        }
    }

    public interface IMessageStoreConfig
    {
        string ConnectionString { get; set; }

        string TableName { get; set; }
    }
}
