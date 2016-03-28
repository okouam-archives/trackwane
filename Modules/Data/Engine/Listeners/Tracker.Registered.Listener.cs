using System;
using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using IProvideTransactions = Trackwane.Framework.Interfaces.IProvideTransactions;
using IRepository = Trackwane.Framework.Interfaces.IRepository;

namespace Trackwane.Data.Engine.Listeners
{
    public class TrackerRegisteredListener : TransactionalListener<TrackerRegistered>
    {
        public TrackerRegisteredListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(TrackerRegistered cmd, IRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}
