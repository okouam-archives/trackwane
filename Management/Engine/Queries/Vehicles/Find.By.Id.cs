using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Domain;
using Trackwane.Management.Responses.Vehicles;

namespace Trackwane.Management.Queries.Vehicles
{
    public class FindById : Query<VehicleDetails>, IScopedQuery
    {
        public VehicleDetails Execute(string vehicleId)
        {
            return Execute(repository =>
            {
                var vehicle = repository.Load<Vehicle>(vehicleId);

                if (vehicle == null) return null;

                var trackerHardwareId = string.Empty;

                if (vehicle.TrackerId != null)
                {
                    var tracker = repository.Load<Tracker>(vehicle.TrackerId);

                    if (tracker != null)
                    {
                        trackerHardwareId = tracker.HardwareId;
                    }
                }

                var driverName = string.Empty;

                if (vehicle.DriverId != null)
                {
                    var driver = repository.Load<Driver>(vehicle.DriverId);

                    if (driver != null)
                    {
                        driverName = driver.Name;
                    }
                }

                return new VehicleDetails
                {
                    Identifier = vehicle.Identifier,
                    IsArchived = vehicle.IsArchived,
                    TrackerId = vehicle.TrackerId,
                    DriverId = vehicle.DriverId,
                    DriverName = driverName,
                    TrackerHardwareId = trackerHardwareId
                };
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
