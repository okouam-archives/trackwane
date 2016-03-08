using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Users;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class ArchiveUserValidator : AbstractValidator<ArchiveUser>
    {
        public ArchiveUserValidator()
        {
            RuleFor(cmd => cmd.UserKey).NotEmpty();

            RuleFor(cmd => cmd.UserKey).Must((cmd, userId) => cmd.RequesterId != userId);
        }
    }
}

