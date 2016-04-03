namespace Trackwane.Framework.Common.Interfaces
{
    public interface IPlatformConfig
    {
        string Get(string key);

        void Set(string key, string value);
    }
}