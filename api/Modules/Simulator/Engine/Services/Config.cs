namespace Trackwane.Simulator.Engine.Services
{
    public class Config : IConfig
    {
        public string ApiKey
        {
            get
            {
                return "tPhNNj5pvWbnW6yYvWpbx9Ssx";
            }
        }

        public string TrackwaneUri
        {
            get
            {
                return "http://api.trackwane.io";
            }
        }

        public string ConsumerUri
        {
            get
            {
                return "http://www.ctabustracker.com/bustime/api/v1";
            }
        }

        public string Organization
        {
            get
            {
                return "Chicago Transport Authority";
            }
        } 
    }
}
