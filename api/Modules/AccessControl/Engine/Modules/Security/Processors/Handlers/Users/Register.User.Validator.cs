using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Users;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator() 
        {
            RuleFor(cmd => cmd.DisplayName).NotEmpty();

            RuleFor(cmd => cmd.Email).NotEmpty();

            RuleFor(cmd => cmd.Password).NotEmpty();
        }
    }
}
