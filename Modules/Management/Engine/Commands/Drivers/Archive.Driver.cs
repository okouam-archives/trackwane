using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Drivers
{
    public class ArchiveDriver : UserCommand

    {
        public string DriverId { get; private set; }

        public string OrganizationId { get; private set; }

        public ArchiveDriver(string applicationKey, string requesterId, string organizationId, string driverId) : base(applicationKey, requesterId)
        {
            DriverId = driverId;
            OrganizationId = organizationId;
        }
    }
}
