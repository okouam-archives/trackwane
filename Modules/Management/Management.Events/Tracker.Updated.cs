using Geo.Geometries;
using Trackwane.Framework.Common;

namespace Trackwane.Management.Events
{
    public class TrackerUpdated : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string TrackerKey { get; set; }

        public State Previous { get; set; }

        public State Current { get; set; }

        public class State
        {
            public decimal? Distance { get; set; }

            public double? BatteryLevel { get; set; }

            public double? Orientation { get; set; }

            public Point Coordinates { get; set; }

            public double? Speed { get; set; }

            public double? Heading { get; set; }

            public double? Petrol { get; set; }
        }
    }
}
