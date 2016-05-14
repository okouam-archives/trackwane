using System.Collections.Generic;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Vehicles;
using Trackwane.Management.Engine.Services;
using log4net;

namespace Trackwane.Management.Engine.Handlers.Vehicles
{
    public class AssignTrackerToVehicleHandler : TransactionalHandler<AssignTrackerToVehicle>
    {
        public AssignTrackerToVehicleHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(AssignTrackerToVehicle cmd, IRepository repository)
        {
            var vehicle = repository.Find<Vehicle>(cmd.VehicleId, cmd.ApplicationKey);

            if (vehicle == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_VEHICLE, cmd.VehicleId));
            }

            var tracker = repository.Find<Sensor>(cmd.TrackerId, cmd.ApplicationKey);

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
