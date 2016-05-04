using System;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class PlatformConfig : Config, IPlatformConfig
    {
        public string Get(string key)
        {
            return null;
        }

        public string SecretKey
        {
            get { return Get("secret-key"); }
        }
    }
}
