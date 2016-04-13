using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            For<IPlatformConfig>().Use<PlatformConfig>();

            For<IProvideTransactions>().Use<TransactionProvider>();

            For<IUnitOfWork>().Use<UnitOfWork>();

            For<ILog>().Use(LogProvider.GetCurrentClassLogger());
        }
    }
}
