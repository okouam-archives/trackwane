using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class PlatformConfig : IPlatformConfig
    {
        public string SecretKey { get; } = ConfigUtils.Get("platform:secret-key");
    }
}
