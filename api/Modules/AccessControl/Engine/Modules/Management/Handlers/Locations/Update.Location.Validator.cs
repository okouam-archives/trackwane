using FluentValidation;
using Trackwane.Management.Engine.Commands.Locations;

namespace Trackwane.Management.Engine.Handlers.Locations
{
    public class UpdateLocationValidator : AbstractValidator<UpdateLocation>
    {
        public UpdateLocationValidator()
        {
            RuleFor(cmd => cmd.LocationId).NotEmpty();
        }
    }
}