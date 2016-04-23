using System.IO;

namespace Trackwane.Framework.Client
{
    public class MetricScope
    {
        private readonly ContextClient client;

        public MetricScope(ContextClient client)
        {
            this.client = client;
        }

        public int Published<Y>()
        {
            var metrics = client.GetMetrics();

            var reader = new StringReader(metrics);

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null) return 0;

                if (line.StartsWith("event_published_success_count") && line.Contains(typeof(Y).Name))
                {
                    var delimiter = line.LastIndexOf(" ");
                    var count = int.Parse(line.Substring(delimiter + 1));
                    return count;
                }
            }
        }
    }
}