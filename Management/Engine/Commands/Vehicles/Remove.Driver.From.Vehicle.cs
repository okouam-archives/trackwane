using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Vehicles
{
    public class RemoveDriverFromVehicle : UserCommand
    {
        public string OrganizationId { get; }

        public string DriverId { get; }

        public string VehicleId { get; }

        public RemoveDriverFromVehicle(string requesterId, string organizationId, string driverId, string vehicleId) : base(requesterId)
        {
            OrganizationId = organizationId;
            DriverId = driverId;
            VehicleId = vehicleId;
        }
    }
}