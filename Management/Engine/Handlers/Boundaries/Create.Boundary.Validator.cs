using FluentValidation;
using Trackwane.Management.Commands.Boundaries;

namespace Trackwane.Management.Handlers.Boundaries
{
    public class CreateBoundaryValidator : AbstractValidator<CreateBoundary>
    {
        public CreateBoundaryValidator()
        {
            RuleFor(cmd => cmd.BoundaryId).NotEmpty();

            RuleFor(cmd => cmd.Name).NotEmpty();

            RuleFor(cmd => cmd.Coordinates).NotNull();

            RuleFor(cmd => cmd.OrganizationId).NotEmpty();
        }
    }
}