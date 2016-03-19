using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Trackwane.Simulator.Engine
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly StreamWriter writer;

        public LoggingHandler(Stream stream)
        {
            writer = new StreamWriter(stream);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            Log.Debug("{0}\t{1}\t{2}", request.RequestUri, (int)response.StatusCode, response.Headers.Date);
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                writer.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
