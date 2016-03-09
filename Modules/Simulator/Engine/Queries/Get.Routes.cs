using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using log4net;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Engine.Queries
{
    public class GetRoutes
    {
        private readonly IConfig config;

        public GetRoutes(IConfig config)
        {
            this.config = config;
        }

        public IEnumerable<string> Execute()
        {
            var url = $"{config.ConsumerUri}/getroutes?key={config.ApiKey}";

            log.Debug(url);

            var result = new HttpClient().GetAsync(url).Result;

            var content = result.Content.ReadAsStringAsync().Result;

            log.Debug(content);

            var doc = XDocument.Parse(content);

            return from route in doc.Descendants("route")
                   let val = route.Element("rt").Value
                   orderby val
                   select val;
        }

        private readonly ILog log = LogManager.GetLogger(typeof(GetRoutes));

  
    }
}
