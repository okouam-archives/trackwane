using System.Collections.Generic;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Vehicles;
using Message = Trackwane.Management.Engine.Services.Message;
using log4net;

namespace Trackwane.Management.Engine.Handlers.Vehicles
{
    public class RemoveDriverFromVehicleHandler : TransactionalHandler<RemoveDriverFromVehicle>
    {
        public RemoveDriverFromVehicleHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RemoveDriverFromVehicle cmd, IRepository repository)
        {
            var driver = repository.Find<Driver>(cmd.DriverId, cmd.ApplicationKey);

            if (driver == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_DRIVER, cmd.DriverId));
            }

            var vehicle = repository.Find<Vehicle>(cmd.VehicleId, cmd.ApplicationKey);

            if (vehicle == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_VEHICLE, cmd.VehicleId));
            }

            vehicle.UnassignDriver(driver);

            return vehicle.GetUncommittedChanges();
        }
    }
}
