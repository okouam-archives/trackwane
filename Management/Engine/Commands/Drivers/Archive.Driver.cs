using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Commands.Drivers
{
    public class ArchiveDriver : UserCommand

    {
        public string DriverId { get; }

        public string OrganizationId { get; }

        public ArchiveDriver(string requesterId, string organizationId, string driverId) : base(requesterId)
        {
            DriverId = driverId;
            OrganizationId = organizationId;
        }
    }
}
