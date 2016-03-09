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
    public class ArchiveVehicleHandler : TransactionalHandler<ArchiveVehicle>
    {
        public ArchiveVehicleHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }
        
        protected override IEnumerable<DomainEvent> Handle(ArchiveVehicle cmd, IRepository repository)
        {
            var vehicle = repository.Load<Vehicle>(cmd.VehicleId);

            if (vehicle == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_VEHICLE, cmd.VehicleId));
            }

            vehicle.Archive();

            return vehicle.GetUncommittedChanges();
        }
    }
}
