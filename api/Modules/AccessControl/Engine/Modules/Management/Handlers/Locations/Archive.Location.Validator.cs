using FluentValidation;
using Trackwane.Management.Engine.Commands.Locations;

namespace Trackwane.Management.Engine.Handlers.Locations
{
    public class ArchiveLocationValidator : AbstractValidator<ArchiveLocation>
    {
        public ArchiveLocationValidator()
        {
            RuleFor(cmd => cmd.LocationId)
                .NotEmpty();
        }
    }
}