using System;
using System.IO;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Storage
{
    public class DocumentStoreBuilder : IDocumentStoreBuilder
    {
        private readonly IModuleConfig config;

        public DocumentStoreBuilder(IModuleConfig config)
        {
            this.config = config;
        }

        public IDocumentStore CreateDocumentStore()
        {
            IDocumentStore store;

            if (bool.Parse(config.Get("document-store/use-embedded")))
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
                    DefaultDatabase = config.Get("document-store/name"),
                    Url = config.Get("document-store/url"),
                    ApiKey = config.Get("document-store/api-key")
                };
            }

            store.Initialize();

            return store;
        }
    }
}
