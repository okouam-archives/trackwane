namespace Trackwane.Framework.Common.Configuration
{
    public class ModuleConfig
    {
        public string Uri { get; } = ConfigUtils.Get("module:uri");
    }
}
