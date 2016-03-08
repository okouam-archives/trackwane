using FluentValidation;
using Trackwane.Management.Commands.Locations;

namespace Trackwane.Management.Handlers.Locations
{
    public class RegisterLocationValidator : AbstractValidator<RegisterLocation>
    {
        public RegisterLocationValidator()
        {
            RuleFor(cmd => cmd.LocationId).NotEmpty();

            RuleFor(cmd => cmd.Name).NotEmpty();

            RuleFor(cmd => cmd.Coordinates).NotNull();

            RuleFor(cmd => cmd.OrganizationId).NotEmpty();
        }
    }
}