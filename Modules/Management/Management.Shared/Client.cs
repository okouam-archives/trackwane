using Trackwane.Framework.Client;
using Trackwane.Management.Contracts.Scopes;

namespace Trackwane.Management.Contracts
{
    public class ManagementContext : ContextClient<ManagementContext>
    {
        private AlertScope alerts;
        private TrackerScopeContext trackers;
        private VehicleScope vehicles;
        private BoundaryScope boundaries;
        private LocationScope locations;
        private MetricScope metrics;

        public VehicleScope Vehicles
        {
            get { return vehicles ?? (vehicles = new VehicleScope(this)); }
        }

        public AlertScope Alerts
        {
            get { return alerts ?? (alerts = new AlertScope(this)); }
        }

        public LocationScope Locations
        {
            get { return locations ?? (locations = new LocationScope(this)); }
        }

        public TrackerScopeContext Trackers
        {
            get { return trackers ?? (trackers = new TrackerScopeContext(this)); }
        }

        public BoundaryScope Boundaries
        {
            get { return boundaries ?? (boundaries = new BoundaryScope(this)); }
        }

        public MetricScope Metrics
        {
            get
            {
                return metrics ?? (metrics = new MetricScope(this));
            }
        }

        public ManagementContext(string server, string protocol, string apiPort, string secretKey, string metricsPort, string applicationKey) : base(server, protocol, apiPort, secretKey, metricsPort, applicationKey)
        {
        }
    }
}