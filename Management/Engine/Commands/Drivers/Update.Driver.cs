using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Drivers
{
    public class UpdateDriver : UserCommand
    {
        public string DriverId { get; }

        public string Name { get; set; }

        public UpdateDriver(string requesterId, string organizationId, string driverId) : base(requesterId)
        {
            DriverId = driverId;
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}
