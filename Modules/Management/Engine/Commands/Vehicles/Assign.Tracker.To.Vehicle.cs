using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class AssignTrackerToVehicle : UserCommand
    {
        public string VehicleId { get; private set; }

        public string TrackerId { get; private set; }

        public string OrganizationId { get; private set; }

        public AssignTrackerToVehicle(string applicationKey, string requesterId, string organizationId, string vehicleId, string trackerId) : base(applicationKey, requesterId)
        {
            OrganizationId = organizationId;
            VehicleId = vehicleId;
            TrackerId = trackerId;
        }
    }
}
