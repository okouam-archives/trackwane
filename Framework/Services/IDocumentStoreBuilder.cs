using Marten;

namespace Trackwane.Framework.Interfaces
{
    public interface IDocumentStoreBuilder
    {
        IDocumentStore CreateDocumentStore();
    }
}