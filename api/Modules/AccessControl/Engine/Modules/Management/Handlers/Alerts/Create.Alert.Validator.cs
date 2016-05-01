using FluentValidation;
using Trackwane.Management.Engine.Commands.Alerts;

namespace Trackwane.Management.Engine.Handlers.Alerts
{
    public class CreateAlertValidator : AbstractValidator<CreateAlert>
    {
        public CreateAlertValidator()
        {
            RuleFor(cmd => cmd.AlertKey).NotEmpty();

            RuleFor(cmd => cmd.Name).NotEmpty();

            RuleFor(cmd => cmd.OrganizationKey).NotEmpty();
        }
    }
}