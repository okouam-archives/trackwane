namespace Trackwane.Management.Engine.Services
{
    public struct Message
    {
        public const string UNKNOWN_ALERT = "The alert with ID <{0}> could not be found";
        public const string UNKNOWN_BOUNDARY = "The boundary with ID <{0}> could not be found";
        public const string UNKNOWN_DRIVER = "The driver with ID <{0}> could not be found";
        public const string UNKNOWN_VEHICLE = "The vehicle with ID <{0}> could not be found";
        public const string UNKNOWN_TRACKER = "The tracker with ID <{0}> could not be found";
        public const string UNKNOWN_LOCATION = "The location with ID <{0}> could not be found";
        public const string UNKNOWN_ORGANIZATION = "The organization with ID <{0}> could not be found";

        public const string DUPLICATE_ALERT_NAME = "The name <{0}> is already in use by another alert";
        public const string DUPLICATE_BOUNDARY_NAME = "The name <{0}> is already in use by another boundary";
        public const string DUPLICATE_DRIVER_NAME = "The name <{0}> is already in use by another driver";
        public const string DUPLICATION_LOCATION_NAME = "The name <{0}> is already in use by another location";
        public const string DUPLICATION_VEHICLE_IDENTIFIER = "The identifier <{0}> is already in use by another vehicle";
        public const string DUPLICATION_TRACKER_HARDWARE_ID = "The hardware ID <{0}> is already in use by another tracker";
    }
}
