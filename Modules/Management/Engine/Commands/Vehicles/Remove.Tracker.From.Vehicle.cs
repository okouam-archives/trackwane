using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class RemoveTrackerFromVehicle : UserCommand
    {
        public string OrganizationId { get; }

        public string TrackerId { get; }

        public string VehicleId { get; }

        public RemoveTrackerFromVehicle(string requesterId, string organizationId, string trackerId, string vehicleId) : base(requesterId)
        {
            OrganizationId = organizationId;
            TrackerId = trackerId;
            VehicleId = vehicleId;
        }
    }
}