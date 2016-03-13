namespace Trackwane.Framework.Common.Interfaces
{
    public interface IDocumentStoreConfig
    {
        bool UseEmbedded { get; }

        string Name { get; }

        string Url { get; }

        string ApiKey { get; }
    }
}

