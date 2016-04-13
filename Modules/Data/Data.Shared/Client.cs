using Trackwane.Data.Shared.Models;
using Trackwane.Framework.Client;
using Trackwane.Framework.Common.Interfaces;

// ReSharper disable CheckNamespace
namespace Trackwane.Data.Shared.Client
// ReSharper restore CheckNamespace
{
    public class DataContext : ContextClient<DataContext>
    {
        public DataContext(string baseUrl, IPlatformConfig config) : base(baseUrl, config)
        {
            
        }

        public void SaveSensorReading(SaveSensorReadingModel model)
        {
            this.POST(this.Expand("data"), model);
        }
    }
}