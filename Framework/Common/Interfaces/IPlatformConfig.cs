namespace Trackwane.Framework.Common.Interfaces
{
    public interface IPlatformConfig
    {
        string Get(string key);

        string SecretKey { get; }
    }
}