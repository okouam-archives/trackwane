using Trackwane.AccessControl.Contracts.Scopes;
using Trackwane.Data.Shared.Models;
using Trackwane.Framework.Client;
using Trackwane.Management.Contracts.Scopes;

namespace Trackwane.Contracts
{
    public class TrackwaneClient : ContextClient<TrackwaneClient>
    {
        private UserScope users;
        private OrganizationScope organizations;
        private ApplicationScope application;
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


        public ApplicationScope Application
        {
            get { return application ?? (application = new ApplicationScope(this)); }
        }

        public UserScope Users
        {
            get { return users ?? (users = new UserScope(this)); }
        }
        
        public OrganizationScope Organizations
        {
            get { return organizations ?? (organizations = new OrganizationScope(this)); }
        }

        public MetricScope Metrics
        {
            get
            {
                return metrics ?? (metrics = new MetricScope(this));
            }
        }

        public TrackwaneClient(string server, string protocol, string apiPort, string secretKey, string metricsPort, string applicationKey) 
            : base(server, protocol, apiPort, secretKey, metricsPort, applicationKey)
        {

        }

        public void SaveSensorReading(SaveSensorReadingModel model)
        {
            this.POST(this.Expand("data"), model);
        }
    }
}
