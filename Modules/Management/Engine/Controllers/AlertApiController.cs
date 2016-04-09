using System.Web.Http;
using Trackwane.Framework.Common;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Engine.Commands.Alerts;
using Trackwane.Management.Engine.Queries.Alerts;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class AlertApiController : BaseManagementController
    {
        private const string RESOURCE_URL = "alerts/{key}";
        private const string COLLECTION_URL = "alerts";

        public AlertApiController(IExecutionEngine dispatcher) : base(dispatcher)
        {
            
        }

        [Secured, Managers, HttpPost, Route(RESOURCE_URL)]
        public void UpdateAlert(string organizationKey, string key, UpdateAlertModel model)
        {
            dispatcher.Send(new UpdateAlert(CurrentClaims.UserId, organizationKey, key, model.Name));
        }       
        
        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public AlertDetails FindById(string organizationKey, string key)
        {
            return dispatcher.Query<FindByKey>(organizationKey).Execute(key);
        }

        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<AlertSummary> FindBySearchCriteria(string organizationKey, SearchAlertsModel model)
        {
            return dispatcher.Query<FindBySearchCriteria>(organizationKey).Execute(model.Name);
        }

        [Secured, Viewers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveAlert(string organizationKey, string key)
        {
            dispatcher.Send(new ArchiveAlert(CurrentClaims.UserId, organizationKey, key));
        }

        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void CreateAlert(string organizationKey, CreateAlertModel model)
        {
            dispatcher.Send(new CreateAlert(CurrentClaims.UserId, organizationKey, model.Name, model.Key));
        }
    }
}
