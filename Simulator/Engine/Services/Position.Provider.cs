﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using log4net;
using Trackwane.Simulator.Domain;

namespace Trackwane.Simulator.Engine.Services
{
    public class PositionProvider : IProvidePositions
    {
        private readonly IConfig config;

        public PositionProvider(IConfig config)
        {
            this.config = config;
        }

        public IEnumerable<Position> Retrieve(string type, params string[] items)
        {
            if (items.Length > 10) throw new Exception("No more than 10 items can be requested at any time");

            var url = $"{config.ConsumerUri}/getvehicles?key={config.ApiKey}&{type}={string.Join(",", items)}";

            log.Debug(url);

            var result = new HttpClient().GetAsync(url).Result;

            return ParsePositions(result);
        }

        private static IEnumerable<Position> ParsePositions(HttpResponseMessage result)
        {
            var doc = XDocument.Parse(result.Content.ReadAsStringAsync().Result);

            return from v in doc.Descendants("vehicle")
                   select new Position(
                        int.Parse(v.Element("vid").Value),
                        decimal.Parse(v.Element("lat").Value),
                        decimal.Parse(v.Element("lon").Value),
                        int.Parse(v.Element("hdg").Value),
                        ParseTimestamp(v.Element("tmstmp").Value));
        }

        private static DateTime ParseTimestamp(string value)
        {
            var parts = value.Split(' ');
            var date = parts[0];
            var time = parts[1];
            var timeParts = time.Split(':');

            return new DateTime(
                int.Parse(date.Substring(0, 4)),
                int.Parse(date.Substring(4, 2)),
                int.Parse(date.Substring(6, 2)),
                int.Parse(timeParts[0]),
                int.Parse(timeParts[1]),
                0);
        }

        private readonly ILog log = LogManager.GetLogger(typeof(PositionProvider));


    }
}
