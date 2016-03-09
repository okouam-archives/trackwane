using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Vehicles;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Vehicles
{
    public class AssignTrackerToVehicleHandler : TransactionalHandler<AssignTrackerToVehicle>
    {
        public AssignTrackerToVehicleHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(AssignTrackerToVehicle cmd, IRepository repository)
        {
            var vehicle = repository.Load<Vehicle>(cmd.VehicleId);

            if (vehicle == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_VEHICLE, cmd.VehicleId));
            }

            var tracker = repository.Load<Tracker>(cmd.TrackerId);

            if (tracker == null)
            {
                throw new BusinessRuleException();
            }

            if (tracker.IsArchived)
            {
                throw new BusinessRuleException();
            }

            if (tracker.OrganizationKey != vehicle.OrganizationKey)
            {
                throw new BusinessRuleException();
            }

            vehicle.AssignTracker(tracker.Key);

            return vehicle.GetUncommittedChanges();
        }
    }
}
