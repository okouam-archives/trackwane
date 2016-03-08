namespace Trackwane.Framework.Interfaces
{
    public interface IDocumentStoreConfig
    {
        bool UseEmbedded { get; }

        string Name { get; }

        string Url { get; }
    }
}
