using Trackwane.Framework.Common.Configuration;

namespace Trackwane.Framework.Common.Interfaces
{
    public interface IModuleConfig
    {
        string Get(string key);

        string ModuleName { get; }

        string ConnectionString { get; }

        ApiConfig ApiConfig { get; }

        MetricConfig MetricConfig { get; }
    }
}