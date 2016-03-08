using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Users;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(cmd => cmd.UserKey).NotEmpty();
        }
    }
}
