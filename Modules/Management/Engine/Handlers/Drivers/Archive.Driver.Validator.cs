using FluentValidation;
using Trackwane.Management.Engine.Commands.Drivers;

namespace Trackwane.Management.Engine.Handlers.Drivers
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