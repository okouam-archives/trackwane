using FluentValidation;
using Trackwane.Management.Commands.Drivers;

namespace Trackwane.Management.Handlers.Drivers
{
    public class RegisterDriverValidator : AbstractValidator<RegisterDriver>
    {
        public RegisterDriverValidator()
        {
            RuleFor(cmd => cmd.OrganizationKey).NotEmpty();

            RuleFor(cmd => cmd.Name).NotEmpty();

            RuleFor(cmd => cmd.DriverKey).NotEmpty();
        }
    }
}