using FluentValidation;
using Trackwane.AccessControl.Contracts.Contracts;

namespace Trackwane.AccessControl.Contracts.Validators
{
    public class RegisterApplicationRequestValidator : AbstractValidator<RegisterApplicationRequest>
    {
        public RegisterApplicationRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.SecretKey).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
