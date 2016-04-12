using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class PlatformConfig : Config, IPlatformConfig
    {
        public string Get(string key)
        {
            var response = client.Get("trackwane/platform/" + key);
            return response.Node.Value;
        }

        public string SecretKey
        {
            get { return Get("secret-key"); }
        }
    }
}
