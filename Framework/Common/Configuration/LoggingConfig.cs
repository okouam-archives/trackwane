using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class LoggingConfig : ILoggingConfig
    {
        public string Uri { get; } = ConfigUtils.Get("logging:uri");
    }
}
