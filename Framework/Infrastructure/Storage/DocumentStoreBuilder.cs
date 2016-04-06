using System;
using System.Configuration;
using System.IO;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure.Storage
{
    public class DocumentStoreBuilder : IDocumentStoreBuilder
    {
        private readonly IConfig config;

        public DocumentStoreBuilder(IConfig config)
        {
            this.config = config;
        }

        public IDocumentStore CreateDocumentStore()
        {
            IDocumentStore store;

            if (bool.Parse(config.GetModuleKey("document-store/use-embedded")))
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
                    DefaultDatabase = config.GetModuleKey("document-store/name"),
                    Url = config.GetModuleKey("document-store/url"),
                    ApiKey = config.GetModuleKey("document-store/api-key")
                };
            }

            store.Initialize();

            return store;
        }
    }
}
