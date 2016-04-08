using System.Configuration;
using Trackwane.Framework.Common.Exceptions;
using static System.String;

namespace Trackwane.Framework.Common.Configuration
{
    internal class ConfigUtils
    {
        public static string Get(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (IsNullOrEmpty(value))
            {
                throw new InvalidConfigurationException("The application setting key <" + name + "> could not be found in the configuration file");
            }

            return value;
        }

        public static bool GetBoolean(string name)
        {
            return bool.Parse(Get(name));
        }
    }
}
