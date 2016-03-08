using FluentValidation;
using Trackwane.Management.Commands.Drivers;

namespace Trackwane.Management.Handlers.Drivers
{
    public class ArchiveDriverValidator : AbstractValidator<ArchiveDriver>
    {
        public ArchiveDriverValidator()
        {
            RuleFor(cmd => cmd.OrganizationId).NotEmpty();

            RuleFor(cmd => cmd.DriverId).NotEmpty();
        }
    }
}