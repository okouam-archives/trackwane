using Geo.Geometries;

// ReSharper disable CheckNamespace
namespace Trackwane.Management.Contracts.Models
// ReSharper restore CheckNamespace
{
    public enum AlertType
    {
        Speed = 0,
        Battery = 2,
        Petrol = 1
    }

    public class AlertDetails
    {
        public bool IsArchived { get; set; }
        public string Name { get; set; }
        public int Threshold { get; set; }
        public string Type { get; set; }
    }

    public class AlertSummary
    {
        public string Key { get; set; }
    }

    public class CreateAlertModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
    }

    public class UpdateAlertModel
    {
        public string Name { get; set; }
    }

    public class SearchAlertsModel
    {
        public string Name { get; set; }
    }

    public class BoundaryDetails
    {
        public Polygon Coordinates { get; set; }
        public bool IsArchived { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class BoundarySummary
    {
        public string OrganizationKey { get; set; }
    }

    public class CreateBoundaryModel
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public Polygon Coordinates { get; set; }

        public string Type { get; set; }
    }

    public class SearchBoundariesModel
    {
        public string Name { get; set; }
    }

    public class UpdateBoundaryModel
    {
        public string Name { get; set; }

        public Polygon Coordinates { get; set; }
    }

    public class CreateDriverModel
    {
        public string Key { get; set; }

        public string Name { get; set; }
    }

    public class DriverDetails
    {
        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public string Id { get; set; }
    }

    public class DriverSummary
    {
        public string Key { get; set; }
    }

    public class DriverSearchCriteriaModel
    {
        public string Name { get; set; }
    }

    public class UpdateDriverModel
    {
        public string Name { get; set; }
    }

    public class RegisterLocationModel
    {
        public string Name { get; set; }

        public string Coordinates { get; set; }
    }

    public class LocationDetails
    {
        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public Point Coordinates { get; set; }
    }

    public class LocationSummary
    {
        public string Key { get; set; }
    }

    public class SearchLocationsModel
    {
        public string Name { get; set; }
    }

    public class UpdateLocationModel
    {
        public string Name { get; set; }

        public Point Coordinates { get; set; }
    }

    public class RegisterTrackerModel
    {
        public string Identifier { get; set; }

        public string HardwareId { get; set; }

        public string Model { get; set; }
    }

    public class TrackerSearchCriteriaModel
    {
        public string Name { get; set; }
    }

    public class TrackerDetails
    {
        public string Model { get; set; }

        public bool IsArchived { get; set; }

        public string HardwareId { get; set; }

        public string Id { get; set; }
    }

    public class TrackerSummary
    {
        public bool IsArchived { get; set; }
        public string HardwareId { get; set; }
        public string Key { get; set; }
        public string Model { get; set; }
    }

    public class UpdateTrackerModel
    {
    }

    public class RegisterVehicleModel
    {
        public string Key { get; set; }

        public string Identifier { get; set; }
    }

    public class SearchVehiclesModel
    {
        public string Identifier { get; set; }
    }

    public class UpdateVehicleModel
    {
        public string Identifier { get; set; }
    }

    public class VehicleDetails
    {
        public bool IsArchived { get; set; }

        public string OrganizationId { get; set; }

        public string DriverId { get; set; }

        public string DriverName { get; set; }

        public string TrackerId { get; set; }

        public string TrackerHardwareId { get; set; }

        public string Identifier { get; set; }
    }

    public class VehicleSummary
    {
        public bool IsArchived { get; set; }

        public string OrganizationId { get; set; }

        public string Identifier { get; set; }
    }
}
