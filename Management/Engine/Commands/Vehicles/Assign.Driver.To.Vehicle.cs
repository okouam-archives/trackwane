using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Vehicles
{
    public class AssignDriverToVehicle : UserCommand
    {
        public string VehicleId { get; }

        public string DriverId { get; }

        public string OrganizationId { get; }

        public AssignDriverToVehicle(string requesterId, string organizationId, string vehicleId, string driverId) : base(requesterId)
        {
            OrganizationId = organizationId;
            VehicleId = vehicleId;
            DriverId = driverId;
        }
    }
}
