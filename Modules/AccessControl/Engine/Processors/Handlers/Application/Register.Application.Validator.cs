using FluentValidation;
using Trackwane.AccessControl.Engine.Commands.Application;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Application
{
    public class RegisterApplicationValidator : AbstractValidator<RegisterApplication>
    {
        public RegisterApplicationValidator() 
        {
            RuleFor(cmd => cmd.DisplayName).NotEmpty();

            RuleFor(cmd => cmd.Email).NotEmpty();

            RuleFor(cmd => cmd.Password).NotEmpty();
        }
    }
}
