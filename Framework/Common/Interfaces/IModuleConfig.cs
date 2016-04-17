namespace Trackwane.Framework.Common.Interfaces
{
    public interface IModuleConfig
    {
        string Get(string key);

        void Set(string key, string value);

        string ModuleName { get; }

        string ConnectionString { get; }
    }
}