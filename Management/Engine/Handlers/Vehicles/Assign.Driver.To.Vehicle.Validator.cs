using FluentValidation;
using Trackwane.Management.Commands.Vehicles;

namespace Trackwane.Management.Handlers.Vehicles
{
    public class AssignDriverToVehicleValidator : AbstractValidator<AssignDriverToVehicle>
    {
        public AssignDriverToVehicleValidator()
        {
            RuleFor(cmd => cmd.VehicleId).NotEmpty();

            RuleFor(cmd => cmd.DriverId).NotEmpty();

            RuleFor(cmd => cmd.OrganizationId).NotEmpty();
        }
    }
}