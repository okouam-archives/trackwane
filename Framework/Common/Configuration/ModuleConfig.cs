using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class ModuleConfig : IModuleConfig
    {
        public string Uri { get; } = ConfigUtils.Get("module:uri");
    }
}
