using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Vehicles
{
    public class RegisterVehicle : UserCommand

    {
        public string VehicleId { get; set;  }

        public string Identifier { get; }

        public string OrganizationId { get; }

        public RegisterVehicle(string requesterId, string organizationId, string identifier, string vehicleId = null) : base(requesterId)
        {
            VehicleId = vehicleId;
            Identifier = identifier;
            OrganizationId = organizationId;
        }
    }
}
