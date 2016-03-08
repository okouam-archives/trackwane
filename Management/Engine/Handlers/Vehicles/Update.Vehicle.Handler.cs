using paramore.brighter.commandprocessor;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Logging;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Infrastructure.Validation;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Vehicles;
using Trackwane.Management.Domain;
using Trackwane.Management.Events;
using Message = Trackwane.Management.Services.Message;

namespace Trackwane.Management.Handlers.Vehicles
{
    public class UpdateVehicleHandler : RuntimeRequestHandler<UpdateVehicle>
    {
        public UpdateVehicleHandler(
            IProvideTransactions transaction, 
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        [Log(1, HandlerTiming.Before)]
        [Validate(2, HandlerTiming.Before)]
        public override UpdateVehicle Handle(UpdateVehicle cmd)
        {
            using (var uow = transaction.Begin())
            {
                var repository = uow.GetRepository();

                var vehicle = repository.Load<Vehicle>(cmd.VehicleKey);

                if (vehicle == null)
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_VEHICLE, cmd.VehicleKey));
                }

                var oldIdentifier = vehicle.Identifier;

                vehicle.Update(cmd.Identifier);
                
                Publish(new VehicleUpdated
                {
                    VehicleKey = cmd.VehicleKey,
                    OrganizationKey = cmd.OrganizationKey,
                    Previous = new VehicleUpdated.State
                    {
                      Identifier  = oldIdentifier
                    },
                    Current = new VehicleUpdated.State
                    {
                        Identifier = cmd.Identifier
                    }
                });
        
                uow.Commit();
            }

            return base.Handle(cmd);
        }
    }
}
