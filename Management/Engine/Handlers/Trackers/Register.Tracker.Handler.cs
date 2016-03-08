using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Trackers;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Handlers.Trackers
{
    public class RegisterTrackerHandler : TransactionalHandler<RegisterTracker>
    {
        public RegisterTrackerHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterTracker cmd, IRepository repository)
        {
            var tracker = new Tracker(cmd.TrackerId, cmd.OrganizationId, cmd.HardwareId, cmd.Model);

            repository.Persist(tracker);

            return tracker.GetUncommittedChanges();
        }
    }
}
