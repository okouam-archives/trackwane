using Trackwane.Data.Models;
using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Data.Client
{
    public class DataContext : ContextClient<DataContext>
    {
        public DataContext(string baseUrl, IPlatformConfig platformConfig) : base(baseUrl, platformConfig)
        {
        }

        public void SaveSensorReading(SaveSensorReadingModel model)
        {
            POST(client, Expand("data"), model);
        }
    }
}
