using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Tests.Fakes
{
    public class CheckFrameworkHandler : TransactionalHandler<CheckFramework>
    {
        public CheckFrameworkHandler(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(transaction, engine, log)
        {
        }

        protected override IEnumerable<BusEvent> Handle(CheckFramework cmd, IRepository repository)
        {
            return new List<BusEvent>() {new FrameworkChecked()};
        }
    }
}
