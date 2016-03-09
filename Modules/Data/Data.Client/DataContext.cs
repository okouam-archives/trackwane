using System;
using Trackwane.Data.Models;
using Trackwane.Framework.Client;

namespace Trackwane.Data.Client
{
    public class DataContext : ContextClient<DataContext>
    {
        public DataContext(string baseUrl) : base(baseUrl)
        {
        }

        public void SaveSensorReading(SaveSensorReadingModel registerSensorReading)
        {
            throw new NotImplementedException();
        }
    }
}
