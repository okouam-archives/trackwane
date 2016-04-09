using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Trackers
{
    public class RegisterTracker : UserCommand
    {
        public string TrackerId { get; set;  }

        public string HardwareId { get; private set; }

        public string Model { get; private set; }

        public string OrganizationId { get; private set; }

        public string Identifier { get; private set; }

        public RegisterTracker(string requesterId, string organizationId, string hardwareId, string model, string identifier, string trackerId = null) : base(requesterId)
        {
            TrackerId = trackerId;
            HardwareId = hardwareId;
            OrganizationId = organizationId;
            Model = model;
            Identifier = identifier;
        }
    }
}
