using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class RemoveDriverFromVehicle : UserCommand
    {
        public string OrganizationId { get; private set; }

        public string DriverId { get; private set; }

        public string VehicleId { get; private set; }

        public RemoveDriverFromVehicle(string applicationKey, string requesterId, string organizationId, string driverId, string vehicleId) : base(applicationKey, requesterId)
        {
            OrganizationId = organizationId;
            DriverId = driverId;
            VehicleId = vehicleId;
        }
    }
}