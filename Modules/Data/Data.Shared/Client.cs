using Trackwane.Data.Shared.Models;
using Trackwane.Framework.Client;

// ReSharper disable CheckNamespace
namespace Trackwane.Data.Shared.Client
// ReSharper restore CheckNamespace
{
    public class DataContext : ContextClient<DataContext>
    {
        public DataContext(string server, string protocol, string apiPort, string secretKey, string metricsPort, string applicationKey) : base(server, protocol, apiPort, secretKey, metricsPort, applicationKey)
        {
            
        }

        public void SaveSensorReading(SaveSensorReadingModel model)
        {
            this.POST(this.Expand("data"), model);
        }
    }
}