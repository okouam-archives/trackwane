namespace Trackwane.Framework.Common.Interfaces
{
    public interface IConfig
    {
        string GetModuleKey(string key);

        void SetModuleKey(string key, string value);

        string GetPlatformKey(string secretKey);
    }
}