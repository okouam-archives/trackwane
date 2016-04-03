using System;
using etcetera;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class PlatformConfig : IPlatformConfig
    {
        private readonly EtcdClient client;

        public PlatformConfig()
        {
            client = new EtcdClient(new Uri("http://localhost:4001/v2/keys/"));
        }

        public string Get(string key)
        {
            var response = client.Get(key);
            return response.Node.Value;
        }

        public void Set(string key, string value)
        {
            client.Set(key, value);
        }
    }
}
