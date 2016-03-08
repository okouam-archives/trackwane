using FluentValidation;
using Trackwane.Management.Commands.Drivers;

namespace Trackwane.Management.Handlers.Drivers
{
    public class UpdateDriverValidator : AbstractValidator<UpdateDriver>
    {
        public UpdateDriverValidator()
        {
            RuleFor(cmd => cmd.DriverId).NotEmpty();

            RuleFor(cmd => cmd.OrganizationId).NotEmpty();
        }
    }
}