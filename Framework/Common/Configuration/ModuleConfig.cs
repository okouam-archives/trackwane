using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using etcetera;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class ModuleConfig : Config, IModuleConfig
    {
        private readonly Assembly assembly;

        public string ModuleName
        {
            get
            {
                var attributes = assembly.GetCustomAttributesData()
                    .Where(x => x.AttributeType == typeof (AssemblyMetadataAttribute))
                    .Where(x => x.ConstructorArguments.Count == 2);

                return (string) attributes
                .First(x => (string)x.ConstructorArguments[0].Value == "module")
                .ConstructorArguments[1].Value;
            }
        }
        
        public ModuleConfig(Assembly assembly) 
        {
            this.assembly = assembly;
        }

        public string Uri
        {
            get { return Get("uri"); }
        }

        public DocumentStoreConfig DocumentStore
        {
            get
            {
                return new DocumentStoreConfig(this);
            }
        }

        public string Get(string key)
        {
            var response = client.Get("trackwane/modules/" + ModuleName + "/" + key);
            return response.Node.Value;
        }

        public void Set(string key, string value)
        {
            client.Set("trackwane/modules/" + ModuleName + "/" + key, value);
        }

        public class DocumentStoreConfig
        {
            private readonly ModuleConfig moduleConfig;

            public DocumentStoreConfig(ModuleConfig moduleConfig)
            {
                this.moduleConfig = moduleConfig;
            }

            public bool UseEmbedded
            {
                get { return bool.Parse(moduleConfig.Get("document-store/use-embedded")); }
            }

            public bool Url
            {
                get { return bool.Parse(moduleConfig.Get("document-store/url")); }
            }

            public bool Name
            {
                get { return bool.Parse(moduleConfig.Get("document-store/name")); }
            }

            public bool ApiKey
            {
                get { return bool.Parse(moduleConfig.Get("document-store/api-key")); }
            }
        }
    }
}
