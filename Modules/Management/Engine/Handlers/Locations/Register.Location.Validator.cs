using FluentValidation;
using Trackwane.Management.Engine.Commands.Locations;

namespace Trackwane.Management.Engine.Handlers.Locations
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