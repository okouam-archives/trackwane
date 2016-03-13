using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Management.Client
{
    public partial class ManagementContext : ContextClient<ManagementContext>
    {
        private AlertCommandsAndQueries alerts;
        private BoundaryCommandsAndQueries boundaries;
        private LocationCommandsAndQueries locations;
        private VehicleCommandsAndQueries vehicles;
        private DriverCommandsAndQueries drivers;
        private TrackerCommandsAndQueries trackers;

        public AlertCommandsAndQueries Alerts => alerts ?? (alerts = new AlertCommandsAndQueries(client));

        public BoundaryCommandsAndQueries Boundaries => boundaries ?? (boundaries = new BoundaryCommandsAndQueries(client));
        
        public LocationCommandsAndQueries Locations => locations ?? (locations = new LocationCommandsAndQueries(client));

        public VehicleCommandsAndQueries Vehicles => vehicles ?? (vehicles = new VehicleCommandsAndQueries(client));

        public DriverCommandsAndQueries Drivers => drivers ?? (drivers = new DriverCommandsAndQueries(client));

        public TrackerCommandsAndQueries Trackers => trackers ?? (trackers = new TrackerCommandsAndQueries(client));

        public ManagementContext(string baseUrl, IPlatformConfig platformConfig) : base(baseUrl, platformConfig)
        {
        }
    }
}
