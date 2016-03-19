﻿using System.Web.Http;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Management.Engine.Controllers
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
        public dynamic GetModuleStatus()
        {
            return new
            {
                name = moduleConfig.ServiceName,
                loggingUri = loggingConfig.Uri,
                isPlatformKeyConfigured = !string.IsNullOrWhiteSpace(platformConfig.SecretKey),
                documentStore = documentStoreConfig.UseEmbedded ? "embedded" : documentStoreConfig.Name + "@" + documentStoreConfig.Url
            };
        }
    }
}