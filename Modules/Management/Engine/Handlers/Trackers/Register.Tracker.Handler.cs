using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Trackers;

namespace Trackwane.Management.Engine.Handlers.Trackers
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
