using Marten;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Vehicles
{
    public class FindById : Query<VehicleDetails>, IOrganizationQuery
    {
        public VehicleDetails Execute(string vehicleId)
        {
            return Execute(repository =>
            {
                var vehicle = repository.Find<Vehicle>(vehicleId, ApplicationKey);

                if (vehicle == null) return null;

                var trackerHardwareId = string.Empty;

                if (vehicle.TrackerId != null)
                {
                    var tracker = repository.Find<Tracker>(vehicle.TrackerId, ApplicationKey);

                    if (tracker != null)
                    {
                        trackerHardwareId = tracker.HardwareId;
                    }
                }

                var driverName = string.Empty;

                if (vehicle.DriverId != null)
                {
                    var driver = repository.Find<Driver>(vehicle.DriverId, ApplicationKey);

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
