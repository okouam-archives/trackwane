using System;
using System.Configuration;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class PlatformConfig : IPlatformConfig
    {
        public string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string SecretKey
        {
            get { return Get("TRACKWANE:PLATFORM:SECRET-KEY"); }
        }
    }
}
