using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Vehicles
{
    public class AssignTrackerToVehicle : UserCommand
    {
        public string VehicleId { get; }

        public string TrackerId { get; }

        public string OrganizationId { get; }

        public AssignTrackerToVehicle(string requesterId, string organizationId, string vehicleId, string trackerId) : base(requesterId)
        {
            OrganizationId = organizationId;
            VehicleId = vehicleId;
            TrackerId = trackerId;
        }
    }
}
