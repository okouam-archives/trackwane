using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class UpdateVehicle : UserCommand
    {
        public string VehicleKey { get; }

        public string Identifier { get; }

        public string OrganizationKey { get; }

        public UpdateVehicle(string requesterId, string organizationKey, string vehicleKey, string identifier) : base(requesterId)
        {
            OrganizationKey = organizationKey;
            VehicleKey = vehicleKey;
            Identifier = identifier;
        }
    }
}
