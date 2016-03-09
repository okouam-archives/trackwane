using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Trackers;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Trackers
{
    public class ArchiveTrackerHandler : TransactionalHandler<ArchiveTracker>
    {
        public ArchiveTrackerHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }
        
        protected override IEnumerable<DomainEvent> Handle(ArchiveTracker cmd, IRepository repository)
        {
            var tracker = repository.Load<Tracker>(cmd.TrackerId);

            if (tracker == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_TRACKER, cmd.TrackerId));
            }

            tracker.Archive();

            return tracker.GetUncommittedChanges();
        }
    }
}
