using Geo.Geometries;
using Trackwane.Framework.Common;

// ReSharper disable CheckNamespace
namespace Trackwane.Management.Contracts.Events
// ReSharper restore CheckNamespace
{
    public class AlertArchived : DomainEvent
    {
        public string AlertKey { get; set; }
        public string OrganizationKey { get; set; }
    }

    public class AlertCreated : DomainEvent
    {
        public string AlertKey { get; set; }
        public string Name { get; set; }
        public AlertType Type { get; set; }
        public int Threshold { get; set; }
        public string OrganizationKey { get; set; }
    }

    public class AlertEdited : DomainEvent
    {
        public string AlertKey { get; set; }
        public string OrganizationKey { get; set; }
        public State Previous { get; set; }
        public State Current { get; set; }
        public class State
        {
            public int Threshold { get; set; }
            public AlertType Type { get; set; }
            public string Name { get; set; }
        }
    }

    public enum AlertType
    {
        Speed,
        Petrol,
        Battery
    }

    public class BoundaryArchived : DomainEvent
    {
        public string BoundaryKey { get; set; }
        public string OrganizationKey { get; set; }
    }

    public class BoundaryCreated : DomainEvent
    {
        public string BoundaryKey { get; set; }
        public string Name { get; set; }
        public string OrganizationKey { get; set; }
        public Polygon Coordinates { get; set; }
    }

    public class BoundaryUpdated : DomainEvent
    {
        public string OrganizationKey { get; set; }
        public string BoundaryKey { get; set; }
        public State Current { get; set; }
        public State Previous { get; set; }
        public class State
        {
            public string Name { get; set; }
            public Polygon Coordinates { get; set; }
        }
    }

    public class DriverArchived : DomainEvent
    {
        public string DriverKey { get; set; }
        public string OrganizationKey { get; set; }
    }

    public class DriverRegistered : DomainEvent
    {
        public string OrganizationKey { get; set; }
        public string DriverKey { get; set; }
        public string Name { get; set; }
    }

    public class DriverUpdated : DomainEvent
    {
        public string DriverKey { get; set; }
        public string OrganizationKey { get; set; }
        public State Current { get; set; }
        public State Previous { get; set; }
        public class State
        {public string Name { get; set; }
        }
    }

    public class LocationArchived : DomainEvent
    {
        public string LocationKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class LocationRegistered : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string LocationKey { get; set; }

        public string Name { get; set; }

        public Point Coordinates { get; set; }
    }

    public class LocationUpdated : DomainEvent
    {
        public State Current { get; set; }

        public State Previous { get; set; }

        public string OrganizationKey { get; set; }

        public string LocationKey { get; set; }

        public string Name { get; set; }

        public class State
        {
            public string Name { get; set; }

            public Point Coordinates { get; set; }
        }
    }

    public class TrackerArchived : DomainEvent
    {
        public string TrackerKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class TrackerRegistered : DomainEvent
    {
        public string TrackerKey { get; set; }
        public string OrganizationKey { get; set; }
        public string Model { get; set; }
        public string HardwareId { get; set; }
        public string Identifier { get; set; }
    }

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

    public class VehicleArchived : DomainEvent
    {
        public string VehicleKey { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class VehicleAssignedDriver : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string VehicleKey { get; set; }

        public string DriverKey { get; set; }
    }
    
    public class VehicleAssignedTracker : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string VehicleKey { get; set; }

        public string TrackerKey { get; set; }
    }

    public class VehicleRegistered : DomainEvent
    {
        public string VehicleKey { get; set; }

        public string OrganizationKey { get; set; }

        public string Identifier { get; set; }
    }

    public class VehicleUpdated : DomainEvent
    {
        public string OrganizationKey { get; set; }

        public string VehicleKey { get; set; }

        public State Previous { get; set; }

        public State Current { get; set; }

        public class State
        {
            public string Identifier { get; set; }
        }
    }
}
