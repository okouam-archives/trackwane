using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Organizations;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class GrantViewPermissionValidator : AbstractValidator<GrantViewPermission>
    {
        public GrantViewPermissionValidator()
        {
            RuleFor(cmd => cmd.UserKey).NotEmpty();
    
            RuleFor(cmd => cmd.OrganizationKey).NotEmpty();
        }
    }
}
