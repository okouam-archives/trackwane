using System.Collections.Generic;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Tests.Fakes
{
    public class CheckFrameworkHandler : TransactionalHandler<CheckFramework>
    {
        public CheckFrameworkHandler(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(CheckFramework cmd, IRepository repository)
        {
            return new List<DomainEvent>() {new FrameworkChecked()};
        }
    }
}
