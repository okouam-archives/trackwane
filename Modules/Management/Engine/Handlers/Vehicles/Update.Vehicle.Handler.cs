using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using MassTransit;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;
using System.Threading.Tasks;
using log4net;
using MassTransit;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Vehicles;
using Message = Trackwane.Management.Engine.Services.Message;

namespace Trackwane.Management.Engine.Handlers.Vehicles
{
    public class UpdateVehicleHandler : RuntimeRequestHandler<UpdateVehicle>
    {
        public UpdateVehicleHandler(
            IProvideTransactions transaction, 
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, log)
        {
        }

        public override Task Consume(ConsumeContext<UpdateVehicle> ctx)
        {
            var cmd = ctx.Message;

            using (var uow = transaction.Begin())
            {
                var repository = uow.GetRepository();

                var vehicle = repository.Find<Vehicle>(cmd.VehicleKey, cmd.ApplicationKey);

                if (vehicle == null)
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_VEHICLE, cmd.VehicleKey));
                }

                var oldIdentifier = vehicle.Identifier;

                vehicle.Update(cmd.Identifier);

     

                uow.Commit();

                var events = new[] {new VehicleUpdated
                {
                    VehicleKey = cmd.VehicleKey,
                    OrganizationKey = cmd.OrganizationKey,
                    Previous = new VehicleUpdated.State { Identifier = oldIdentifier },
                    Current = new VehicleUpdated.State { Identifier = cmd.Identifier }
                }};

                Publish(ctx, events);

                return Task.CompletedTask;
            }
        }
    }
}
