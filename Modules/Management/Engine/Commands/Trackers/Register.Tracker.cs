using System.Data.Common;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Management.Engine.Commands.Trackers
{
    public class RegisterTracker : UserCommand
    {
        public string TrackerId { get; set;  }

        public string HardwareId { get; }

        public string Model { get; }

        public string OrganizationId { get; }

        public string Identifier { get; }
        
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
