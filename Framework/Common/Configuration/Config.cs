using System;
using System.Configuration;
using etcetera;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Common.Configuration
{
    public class Config : IConfig
    {
        private readonly EtcdClient client;
        private readonly string env;
        private readonly string module;

        public Config()
        {
            client = new EtcdClient(new Uri(ConfigurationManager.AppSettings["etcd"]));
            module = ConfigurationManager.AppSettings["module"];
            env = ConfigurationManager.AppSettings["env"];
        }

        public string GetModuleKey(string key)
        {
            var response = client.Get("trackwane/" + env + "/modules/" + module + "/" + key);
            return response.Node.Value;
        }

        public void SetModuleKey(string key, string value)
        {
            client.Set("trackwane/" + env + "/modules/" + module + "/" + key, value);
        }

        public string GetPlatformKey(string key)
        {
            var response = client.Get("trackwane/" + env + "/platform/" + key);
            return response.Node.Value;
        }
    }
}
