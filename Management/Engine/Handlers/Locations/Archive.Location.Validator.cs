using FluentValidation;
using Trackwane.Management.Commands.Locations;

namespace Trackwane.Management.Handlers.Locations
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