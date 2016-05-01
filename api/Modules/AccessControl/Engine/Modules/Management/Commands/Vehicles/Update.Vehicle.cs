using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class UpdateVehicle : UserCommand
    {
        public string VehicleKey { get; private set; }

        public string Identifier { get; private set; }

        public string OrganizationKey { get; private set; }

        public UpdateVehicle(string applicationKey, string requesterId, string organizationKey, string vehicleKey, string identifier) : base(applicationKey, requesterId)
        {
            OrganizationKey = organizationKey;
            VehicleKey = vehicleKey;
            Identifier = identifier;
        }
    }
}
