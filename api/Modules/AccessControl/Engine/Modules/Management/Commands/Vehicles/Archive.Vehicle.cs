using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class ArchiveVehicle : UserCommand
    {
        public string VehicleId { get; private set; }

        public string OrganizationId { get; private set; }

        public ArchiveVehicle(string applicationKey, string requesterId, string organizationId, string vehicleId) : base(applicationKey, requesterId)
        {
            OrganizationId = organizationId;
            VehicleId = vehicleId;
        }
    }
}
