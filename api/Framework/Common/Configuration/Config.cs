using System;
using System.Configuration;
using etcetera;

namespace Trackwane.Framework.Common.Configuration
{
    public abstract class Config
    {
        protected readonly EtcdClient client;

        protected Config()
        {
            client = new EtcdClient(new Uri(Etcd));
        }

        public string Environment
        {
            get
            {
                const string key = "TRACKWANE_ENVIRONMENT";
                var uri = ConfigurationManager.AppSettings[key] ?? System.Environment.GetEnvironmentVariable(key);

                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new Exception("The environment variable TRACKWANE_ENVIRONMENT needs to be available and point to a etcd instance");
                }

                return uri;
            }
        }

        public string Etcd
        {
            get
            {
                const string key = "TRACKWANE_ETCD";
                var uri = ConfigurationManager.AppSettings[key] ?? System.Environment.GetEnvironmentVariable(key);

                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new Exception("The environment variable TRACKWANE_ETCD needs to be available and point to a etcd instance");
                }

                return uri;
            }
        }
    }
}
