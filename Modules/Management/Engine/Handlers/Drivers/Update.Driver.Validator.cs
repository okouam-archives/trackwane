using FluentValidation;
using Trackwane.Management.Engine.Commands.Drivers;

namespace Trackwane.Management.Engine.Handlers.Drivers
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