using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Organizations;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class RegisterOrganizationValidator : AbstractValidator<RegisterOrganization>
    {
        public RegisterOrganizationValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
