using System.Collections.Generic;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Vehicles;

namespace Trackwane.Management.Engine.Handlers.Vehicles
{
    public class RegisterVehicleHandler : TransactionalHandler<RegisterVehicle>
    {
        public RegisterVehicleHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) : 
            base(transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterVehicle cmd, IRepository repository)
        {
            var vehicle = new Vehicle(cmd.ApplicationKey, cmd.VehicleId, cmd.OrganizationId, cmd.Identifier);

            repository.Persist(vehicle);

            return vehicle.GetUncommittedChanges();
        }
    }
}
