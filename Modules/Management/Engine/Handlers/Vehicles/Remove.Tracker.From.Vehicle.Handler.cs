using System.Threading.Tasks;
using log4net;
using MassTransit;
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
            base(transaction, log)
        {
        }

        public override Task Consume(ConsumeContext<RemoveTrackerFromVehicle> ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
