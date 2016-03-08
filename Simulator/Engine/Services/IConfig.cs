namespace Trackwane.Simulator.Engine.Services
{
    public interface IConfig
    {
        string ApiKey { get; }
        string TrackwaneUri { get; }
        string ConsumerUri { get; }
        string Organization { get; }
    }
}