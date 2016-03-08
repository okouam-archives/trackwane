using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Organizations;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class RevokeManagePermissionValidator : AbstractValidator<RevokeManagePermission>
    {
    }
}
