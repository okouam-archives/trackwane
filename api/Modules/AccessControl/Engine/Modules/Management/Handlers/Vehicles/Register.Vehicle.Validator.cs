using FluentValidation;
using Trackwane.Management.Engine.Commands.Vehicles;

namespace Trackwane.Management.Engine.Handlers.Vehicles
{
    public class RegisterVehicleValidator : AbstractValidator<RegisterVehicle>
    {
        public RegisterVehicleValidator()
        {
            RuleFor(cmd => cmd.VehicleId).NotEmpty();

            RuleFor(cmd => cmd.Identifier).NotEmpty();
        }
    }
}