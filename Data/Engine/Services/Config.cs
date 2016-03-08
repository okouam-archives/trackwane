namespace Trackwane.Data.Engine.Services
{
    public class Config
    {
        public string DatabaseName { get; private set; }
        public string DatabaseUrl { get; private set; }
        public string PusherAppId { get; set; }
        public string PusherAppKey { get; set; }
        public string PusherAppSecret { get; set; }
    }
}
