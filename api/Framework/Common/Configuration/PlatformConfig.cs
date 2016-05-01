using System;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class PlatformConfig : Config, IPlatformConfig
    {
        public string Get(string key)
        {
            var node = "trackwane/" + Environment + "/platform/" + key;
            var response = client.Get(node);
            if (response.Node == null)
            {
                throw new Exception($"The key <{node}> was not found");
            }
            return response.Node.Value;
        }

        public string SecretKey
        {
            get { return Get("secret-key"); }
        }
    }
}
