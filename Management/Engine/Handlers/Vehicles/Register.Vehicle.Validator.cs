using FluentValidation;
using Trackwane.Management.Commands.Vehicles;

namespace Trackwane.Management.Handlers.Vehicles
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