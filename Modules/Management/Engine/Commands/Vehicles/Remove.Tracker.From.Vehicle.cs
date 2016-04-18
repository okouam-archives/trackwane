using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class RemoveTrackerFromVehicle : UserCommand
    {
        public string OrganizationId { get; private set; }

        public string TrackerId { get; private set; }

        public string VehicleId { get; private set; }

        public RemoveTrackerFromVehicle(string applicationKey, string requesterId, string organizationId, string trackerId, string vehicleId) : base(applicationKey, requesterId)
        {
            OrganizationId = organizationId;
            TrackerId = trackerId;
            VehicleId = vehicleId;
        }
    }
}