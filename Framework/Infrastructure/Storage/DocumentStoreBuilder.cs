using System;
using System.IO;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Storage
{
    public class DocumentStoreBuilder : IDocumentStoreBuilder
    {
        private readonly IDocumentStoreConfig config;

        public DocumentStoreBuilder(IDocumentStoreConfig config)
        {
            this.config = config;
        }

        public IDocumentStore CreateDocumentStore()
        {
            IDocumentStore store;

            if (config.UseEmbedded)
            {
                store = new EmbeddableDocumentStore
                {
                    DataDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())
                };
            }
            else
            {
                store = new DocumentStore
                {
                    DefaultDatabase = config.Name,
                    Url = config.Url
                };
            }

            store.Initialize();

            return store;
        }
    }
}
