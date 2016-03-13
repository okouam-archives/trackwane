using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            For<IProvideTransactions>().Use<TransactionProvider>();

            For<IUnitOfWork>().Use<UnitOfWork>();

            For<ILog>().Use(LogProvider.GetCurrentClassLogger());

            For<IDocumentStoreConfig>().Use<DocumentStoreConfig>().Singleton();

            For<IDocumentStoreBuilder>().Use<DocumentStoreBuilder>().Singleton();

            For<IServiceLocationFactory>().Use<ServiceLocationFactory>().Singleton();
        }
    }
}
