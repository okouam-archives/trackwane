using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Tests.Fakes
{
    public class FrameworkCheckedListener : TransactionalListener<FrameworkChecked>
    {
        public FrameworkCheckedListener(IProvideTransactions transaction, IExecutionEngine engine, ILog log) : base(transaction, engine, log)
        {
        }

        protected override IEnumerable<BusEvent> Handle(FrameworkChecked cmd, IRepository repository)
        {
            return Enumerable.Empty<BusEvent>();
        }
    }
}
