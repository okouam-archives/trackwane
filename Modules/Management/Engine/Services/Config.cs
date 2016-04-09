using System;
using System.Configuration;

namespace Trackwane.Management.Engine.Services
{
    public class Config
    {
        public string DatabaseName
        {
            get
            {
                var value = ConfigurationManager.AppSettings[DB_NAME];

                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("The application setting key <" + DB_NAME+ "> could not be found in the configuration file");
                }

                return value;
                
            }
        }

        public string DatabaseUrl
        {
            get
            {
                var value = ConfigurationManager.AppSettings[DB_URL];

                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("The application setting key <" + DB_URL + "> could not be found in the configuration file");
                }

                return value;

            }
        }

        private const string DB_URL = "management-database-server";
        private const string DB_NAME = "management-database-name";
    }
}
