using FluentValidation;
using Trackwane.Management.Engine.Commands.Vehicles;

namespace Trackwane.Management.Engine.Handlers.Vehicles
{
    public class AssignTrackerToVehicleValidator : AbstractValidator<AssignTrackerToVehicle>
    {
        public AssignTrackerToVehicleValidator()
        {
            RuleFor(cmd => cmd.VehicleId)
                .NotEmpty();

            RuleFor(cmd => cmd.TrackerId)
             .NotEmpty();

            //RuleFor(cmd => cmd.OrganizationId)
            //    .NotEmpty()
            //    .Must(organizationId => OrganizationRules.IsExistingOrganization(repository, organizationId));
        }
    }
}