using Raven.Client;

namespace Trackwane.Framework.Interfaces
{
    public interface IDocumentStoreBuilder
    {
        IDocumentStore CreateDocumentStore();
    }
}