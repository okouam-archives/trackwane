using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Tests.Fakes
{
    public class CheckFrameworkHandler : TransactionalHandler<CheckFramework>
    {
        public CheckFrameworkHandler(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(transaction, engine, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(CheckFramework cmd, IRepository repository)
        {
            return new List<DomainEvent>() {new FrameworkChecked()};
        }
    }
}
