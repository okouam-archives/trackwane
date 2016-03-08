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

            public int? BatteryLevel { get; set; }

            public int? Orientation { get; set; }

            public Point Coordinates { get; set; }

            public int? Speed { get; set; }

            public int? Heading { get; set; }

            public int? Petrol { get; set; }
        }
    }
}
