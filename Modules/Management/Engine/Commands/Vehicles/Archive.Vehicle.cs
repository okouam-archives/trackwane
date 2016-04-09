using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class ArchiveVehicle : UserCommand
    {
        public string VehicleId { get; private set; }

        public string OrganizationId { get; private set; }

        public ArchiveVehicle(string requesterId, string organizationId, string vehicleId) : base(requesterId)
        {
            OrganizationId = organizationId;
            VehicleId = vehicleId;
        }
    }
}
