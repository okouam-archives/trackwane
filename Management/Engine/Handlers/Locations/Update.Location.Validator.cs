using FluentValidation;
using Trackwane.Management.Commands.Locations;

namespace Trackwane.Management.Handlers.Locations
{
    public class UpdateLocationValidator : AbstractValidator<UpdateLocation>
    {
        public UpdateLocationValidator()
        {
            RuleFor(cmd => cmd.LocationId).NotEmpty();
        }
    }
}