using FluentValidation;
using Trackwane.Management.Commands.Boundaries;

namespace Trackwane.Management.Handlers.Boundaries
{
    public class UpdateBoundaryValidator : AbstractValidator<UpdateBoundary>
    {
        public UpdateBoundaryValidator()
        {
            RuleFor(cmd => cmd.OrganizationId).NotEmpty();

            RuleFor(cmd => cmd.BoundaryId).NotEmpty();
        }
    }
}