using Raven.Client;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Vehicles
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

                return new VehicleDetails {
                    IsArchived = vehicle.IsArchived,
                    OrganizationId = vehicle.OrganizationKey,
                    DriverId = vehicle.DriverId,
                    TrackerId = vehicle.TrackerId,
                    TrackerHardwareId = trackerHardwareId,
                    Identifier = vehicle.Identifier,
                    DriverName = driverName
                };
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
