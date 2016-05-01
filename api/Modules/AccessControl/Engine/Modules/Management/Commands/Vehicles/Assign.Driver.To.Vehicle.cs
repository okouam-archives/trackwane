using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class AssignDriverToVehicle : UserCommand
    {
        public string VehicleId { get; private set; }

        public string DriverId { get; private set; }

        public string OrganizationId { get; private set; }

        public AssignDriverToVehicle(string applicationKey, string requesterId, string organizationId, string vehicleId, string driverId) : base(applicationKey, requesterId)
        {
            OrganizationId = organizationId;
            VehicleId = vehicleId;
            DriverId = driverId;
        }
    }
}
