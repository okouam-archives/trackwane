using FluentValidation;
using Trackwane.AccessControl.Engine.Controllers;

namespace Trackwane.AccessControl.Contracts.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator() 
        {
            RuleFor(cmd => cmd.DisplayName).NotEmpty();

            RuleFor(cmd => cmd.Email).NotEmpty();

            RuleFor(cmd => cmd.Password).NotEmpty();
        }
    }
}
