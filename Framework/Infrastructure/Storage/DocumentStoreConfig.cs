using System.Configuration;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Interfaces;
using static System.String;

namespace Trackwane.Framework.Infrastructure.Storage
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

        public bool UseEmbedded => bool.Parse(GetApplicationSetting(USE_EMBEDDED_DOCUMENT_STORE));

        public string Name => GetApplicationSetting(DOCUMENT_STORE_DATABASE_NAME);

        public string Url => GetApplicationSetting(DOCUMENT_STORE_URL);

        /* Private */

        private static string GetApplicationSetting(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (IsNullOrEmpty(value))
            {
                throw new InvalidConfigurationException($"The application setting key <{name}> could not be found in the configuration file");
            }

            return value;
        }

        private const string USE_EMBEDDED_DOCUMENT_STORE = "document-store:use-embedded";
        private const string DOCUMENT_STORE_URL = "document-store:url";
        private const string DOCUMENT_STORE_DATABASE_NAME = "document-store:name";
    }
}
