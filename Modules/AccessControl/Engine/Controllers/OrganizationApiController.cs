using System;
using System.Collections.Generic;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.AccessControl.Engine.Queries.Organizations;
using Trackwane.AccessControl.Models.Oganizations;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;
using Trackwane.Framework.Web.Security;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class OrganizationApiController : BaseApiController
    {
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig platformConfig;

        public OrganizationApiController(IExecutionEngine executionEngine, IPlatformConfig platformConfig)
        {
            this.executionEngine = executionEngine;
            this.platformConfig = platformConfig;
        }

        [Secured, Administrators, HttpGet, Route("organizations/{organizationKey}")]
        public OrganizationDetails FindById(string organizationKey) =>
            executionEngine.Query<FindByKey>(organizationKey).Execute();

        [Secured, SystemManagers, HttpGet, Route("organizations")]
        public List<OrganizationDetails> Find() =>
            executionEngine.Query<Find>().Execute();

        [Secured, Administrators, HttpGet, Route("organizations/count")]
        public int Count() =>
            executionEngine.Query<Count>().Execute();
        
        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users/{userKey}/view")]
        public void GrantViewPermission(string organizationKey, string userKey) =>
            executionEngine.Send(new GrantViewPermission(CurrentClaims.UserId, organizationKey, userKey));
        
        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users/{userKey}/manage")]
        public void GrantManagePermission(string organizationKey, string userKey) =>
            executionEngine.Send(new GrantManagePermission(CurrentClaims.UserId, organizationKey, userKey));
        
        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/view")]
        public void RevokeViewPermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new RevokeViewPermission(CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/manage")]
        public void RevokeManagePermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new RevokeManagePermission(CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/administrate")]
        public void RevokeAdministratePermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new RevokeAdministratePermission(CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, SystemManagers, HttpDelete, Route("organizations/{organizationKey}")]
        public void ArchiveOrganization(string organizationKey) =>
            executionEngine.Send(new ArchiveOrganization(CurrentClaims.UserId, organizationKey));

        [Secured, SystemManagers, HttpPost, Route("organizations")]
        public string RegisterOrganization(RegisterOrganizationModel model)
        {
            var cmd = new RegisterOrganization(CurrentClaims.UserId, model.OrganizationKey, model.Name);
            cmd.OrganizationKey = cmd.OrganizationKey ?? new Hashids(platformConfig.SecretKey).EncodeLong(DateTime.Now.Ticks);
            executionEngine.Send(cmd);
            return cmd.OrganizationKey;
        }
      
        
        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}")]
        public void UpdateOrganization(string organizationKey, UpdateOrganizationModel model)
        {
            executionEngine.Send(new UpdateOrganization(CurrentClaims.UserId, organizationKey, model.Name));
        }
    }
}
