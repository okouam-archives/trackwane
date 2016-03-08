using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Vehicles;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Handlers.Vehicles
{
    public class RegisterVehicleHandler : TransactionalHandler<RegisterVehicle>
    {
        public RegisterVehicleHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterVehicle cmd, IRepository repository)
        {
            var vehicle = new Vehicle(cmd.VehicleId, cmd.OrganizationId, cmd.Identifier);

            repository.Persist(vehicle);

            return vehicle.GetUncommittedChanges();
        }
    }
}
