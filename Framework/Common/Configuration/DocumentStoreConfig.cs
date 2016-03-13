using System.Configuration;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Common.Interfaces;
using static System.String;

namespace Trackwane.Framework.Common.Configuration
{
    public class DocumentStoreConfig : IDocumentStoreConfig
    {
        public DocumentStoreConfig()
        {
            if (UseEmbedded)
            {
                if (!IsNullOrWhiteSpace(ConfigurationManager.AppSettings[DOCUMENT_STORE_DATABASE_NAME]) || !IsNullOrWhiteSpace(ConfigurationManager.AppSettings[DOCUMENT_STORE_URL]))
                {
                    throw new  InvalidConfigurationException("Both name and URL cannot be provided for the document store when using an embedded document store");
                }
            }
        }

        public bool UseEmbedded => ConfigUtils.GetBoolean(USE_EMBEDDED_DOCUMENT_STORE);

        public string Name => ConfigUtils.Get(DOCUMENT_STORE_DATABASE_NAME);

        public string Url => ConfigUtils.Get(DOCUMENT_STORE_URL);

        public string ApiKey => ConfigUtils.Get(API_KEY);

        /* Private */
        
        private const string API_KEY = "document-store:api-key";
        private const string USE_EMBEDDED_DOCUMENT_STORE = "document-store:use-embedded";
        private const string DOCUMENT_STORE_URL = "document-store:url";
        private const string DOCUMENT_STORE_DATABASE_NAME = "document-store:name";
    }
}
