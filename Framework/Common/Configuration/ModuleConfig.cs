using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class ModuleConfig : IModuleConfig
    {
        public string Uri { get; } = ConfigUtils.Get("module:uri");

        public string Name { get; } = ConfigUtils.Get("module:name");

        public string ServiceName { get; } = ConfigUtils.Get("module:service-name");

        public string DisplayName { get; } = ConfigUtils.Get("module:display-name");
    }
}
