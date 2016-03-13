namespace Trackwane.Framework.Common.Interfaces
{
    public interface IModuleConfig
    {
        string Uri { get; }

        string ServiceName { get; }

        string Name { get; }

        string DisplayName { get; }
    }
}