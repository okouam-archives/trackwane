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

        public string ConnectionString
        {
            get
            {
                return "Server=127.0.0.1;Port=5432;Database=trackwane;User Id=trackwane;Password = trackwane;"; 
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
            var identifier = "trackwane/modules/" + ModuleName + "/" + key;
            var response = client.Get(identifier);
            if (response.Node == null)
            {
                throw new Exception($"The key <{identifier}> was not found");
            }
            return response.Node.Value;
        }

        public void Set(string key, string value)
        {
            client.Set("trackwane/modules/" + ModuleName + "/" + key, value);
        }
    }
}
