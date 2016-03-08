using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Vehicles;

namespace Trackwane.Management.Handlers.Vehicles
{
    public class RemoveTrackerFromVehicleHandler : RuntimeRequestHandler<RemoveTrackerFromVehicle>
    {
        public RemoveTrackerFromVehicleHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        public override RemoveTrackerFromVehicle Handle(RemoveTrackerFromVehicle command)
        {
            return base.Handle(command);
        }
    }
}
