using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Engine.Commands.Vehicles;

namespace Trackwane.Management.Engine.Handlers.Vehicles
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
    }
}
