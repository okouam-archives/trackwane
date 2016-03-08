using System.Collections.Generic;
using System.Configuration;
using log4net;
using RestSharp;
using Trackwane.Simulator.Domain;

namespace Trackwane.Simulator.Engine.Services
{
    public class TrackwaneApi : ITrackwaneApi
    {
        public IEnumerable<string> SaveRawData(List<GpsEvent> events)
        {
            log.Debug($"Saving {events.Count} to the Trackwane API");

            var uri = ConfigurationManager.AppSettings["RAW_DATA_ENDPOINT"];

            var client = new RestClient(uri);

            foreach (var evt in events)
            {
                var request = new RestRequest("raw", Method.POST);
                request.AddQueryParameter("hardwareId", evt.HardwareId);
                request.AddQueryParameter("latitude", evt.Latitude.ToString());
                request.AddQueryParameter("longitude", evt.Longitude.ToString());
                request.AddQueryParameter("orientation", evt.Orientation.ToString());
                request.AddQueryParameter("petrol", evt.Petrol.ToString());
                request.AddQueryParameter("speed", evt.Speed.ToString());
                request.AddQueryParameter("timestamp", evt.Timestamp.ToString());

                var result = client.Execute(request);

                yield return result.Content;

            }
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(TrackwaneApi));
    }
}
