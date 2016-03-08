using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Vehicles
{
    public class ArchiveVehicle : UserCommand
    {
        public string VehicleId { get; }

        public string OrganizationId { get; }

        public ArchiveVehicle(string requesterId, string organizationId, string vehicleId) : base(requesterId)
        {
            OrganizationId = organizationId;
            VehicleId = vehicleId;
        }
    }
}
