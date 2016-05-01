using FluentValidation;
using Trackwane.Management.Engine.Commands.Boundaries;

namespace Trackwane.Management.Engine.Handlers.Boundaries
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