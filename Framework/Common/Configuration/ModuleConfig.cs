using System;
using System.Linq;
using System.Reflection;
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

        public ApiConfig ApiConfig
        {
            get
            {
                return new ApiConfig(this);
            }
        }

        public MetricConfig MetricConfig
        {
            get
            {
                return new MetricConfig(this);
            }
        }

        public string ConnectionString
        {
            get
            {
                return Get("connection-string"); 
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

        public string Get(string key)
        {
            var identifier = "trackwane/" + Environment + "/modules/" + ModuleName + "/" + key;
            var response = client.Get(identifier);
            if (response.Node == null)
            {
                throw new Exception($"The key <{identifier}> was not found");
            }
            return response.Node.Value;
        }

        public void Set(string key, string value)
        {
            client.Set("trackwane/" + Environment + "/modules/" + ModuleName + "/" + key, value);
        }
    }

    public class ApiConfig
    {
        private readonly ModuleConfig config;

        public ApiConfig(ModuleConfig config)
        {
            this.config = config;
        }

        public string Port
        {
            get { return config.Get("api-port"); }
        }
    }

    public class MetricConfig
    {
        private readonly ModuleConfig config;

        public MetricConfig(ModuleConfig config)
        {
            this.config = config;
        }

        public string Port
        {
            get { return config.Get("metrics-port"); }
        }
    }
}
