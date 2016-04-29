﻿using System;
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
                var uri = System.Environment.GetEnvironmentVariable("TRACKWANE_ENVIRONMENT");

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
                var uri = System.Environment.GetEnvironmentVariable("TRACKWANE_ETCD");

                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new Exception("The environment variable TRACKWANE_ETCD needs to be available and point to a etcd instance");
                }

                return uri;
            }
        }
    }
}
