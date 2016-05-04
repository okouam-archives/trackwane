using System.Collections.Generic;
using System.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using log4net;

namespace Trackwane.Framework.Tests.Fakes
{
    public class FrameworkCheckedListener : TransactionalListener<FrameworkChecked>
    {
        public FrameworkCheckedListener(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(transaction, engine, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(FrameworkChecked cmd, IRepository repository)
        {
            return Enumerable.Empty<DomainEvent>();
        }
    }
}
