using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Organizations;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class RevokeViewPermissionValidator : AbstractValidator<RevokeViewPermission>
    {
        public RevokeViewPermissionValidator()
        {
            RuleFor(cmd => cmd.UserKey).NotEmpty();

            RuleFor(cmd => cmd.OrganizationKey).NotEmpty();
        }
    }
}
