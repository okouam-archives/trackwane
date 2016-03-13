using System.Text;
using System.Web.Http;
using Trackwane.Framework.Common.Interfaces;
using static System.String;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class StatusApiController : ApiController
    {
        private readonly IPlatformConfig platformConfig;
        private readonly IModuleConfig moduleConfig;
        private readonly IDocumentStoreConfig documentStoreConfig;
        private readonly ILoggingConfig loggingConfig;

        public StatusApiController(IPlatformConfig platformConfig, IModuleConfig moduleConfig, IDocumentStoreConfig documentStoreConfig, ILoggingConfig loggingConfig)
        {
            this.platformConfig = platformConfig;
            this.moduleConfig = moduleConfig;
            this.documentStoreConfig = documentStoreConfig;
            this.loggingConfig = loggingConfig;
        }

        [HttpGet, Route("status")]
        public string GetModuleStatus()
        {
            var msg = new StringBuilder();

            msg.Append($"The module Trackwane.AccessControl is running and listening at <{moduleConfig.Uri}>.");

            msg.Append($"The module is logging to <{loggingConfig.Uri}>.");

            var secretStatus = !IsNullOrWhiteSpace(platformConfig.SecretKey) ? "The Trackwane platform key is set." : "The Trackwane platform key is not set.";
            msg.Append(secretStatus);

            if (documentStoreConfig.UseEmbedded)
            {
                msg.Append("The module is using a in-memory document store");
            }
            else
            {
                msg.Append($"The module is using a document store located at <{documentStoreConfig.Url}> named <{documentStoreConfig.Name}>.");
            }
          

            return msg.ToString();
        }

    }
}
