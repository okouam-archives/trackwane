using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Events;

namespace Trackwane.Data.Domain
{
    public class Tracker : AggregateRoot
    {
        public string OrganizationId { get; set; }

        public string HardwareId { get; internal set; }

        public double? BatteryLevel { get; internal set; }

        public double? Orientation { get; internal set; }

        public double? Speed { get; internal set; }

        public double? Heading { get; internal set; }

        public decimal? Distance { get; internal set; }

        public Point Coordinates { get; internal set; }

        public Tracker()
        {

        }

        public Tracker(string id, string organizationId, string hardwareId)
        {
            Causes(new TrackerRegistered
            {
                TrackerKey = id,
                HardwareId = hardwareId,
                OrganizationKey = organizationId
            });
        }

        public void Update(double? batteryLevel, double? orientation, double? speed, decimal? distance, Point coordinates, double? heading)
        {
            Causes(new TrackerUpdated
            {
                OrganizationKey = OrganizationId,

                TrackerKey = Key,

                Current = new TrackerUpdatedState(coordinates, distance, batteryLevel, null, heading, speed, orientation),

                Previous = new TrackerUpdatedState(Coordinates, Distance, BatteryLevel, null, Heading, Speed, Orientation)
            });
        }

        /* Protected */

        protected override void Apply(DomainEvent evt)
        {
            When((dynamic)evt);
        }

        /* Private */

        private void When(TrackerRegistered evt)
        {
            Key = evt.TrackerKey;
            OrganizationId = evt.OrganizationKey;
            HardwareId = evt.HardwareId;
        }

        private void When(TrackerUpdated evt)
        {
            BatteryLevel = evt.Current.BatteryLevel; 

            Speed = evt.Current.Speed;

            Distance = evt.Current.Distance;

            Coordinates = evt.Current.Coordinates;

            Heading = evt.Current.Heading;
        }
    }
}
