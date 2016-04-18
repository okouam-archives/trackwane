//using System;

using System;
using System.Collections.Generic;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Contracts.Models;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.AccessControl.Engine.Queries.Organizations;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class OrganizationApiController : BaseApiController
    {
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig config;

        public OrganizationApiController(IExecutionEngine executionEngine, IPlatformConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [Secured, Administrators, HttpGet, Route("organizations/{organizationKey}")]
        public OrganizationDetails FindById(string organizationKey)
        {
            return executionEngine.Query<FindByKey>(organizationKey).Execute();
        }

        [Secured, SystemManagers, HttpGet, Route("organizations")]
        public List<OrganizationDetails> Find()
        {
            return executionEngine.Query<Find>(CurrentClaims.ApplicationKey).Execute();
        }

        [Secured, Administrators, HttpGet, Route("organizations/count")]
        public int Count()
        {
            return executionEngine.Query<Count>(CurrentClaims.ApplicationKey).Execute();
        }

        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users/{userKey}/view")]
        public void GrantViewPermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new GrantViewPermission(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, userKey));
        }
           
        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users/{userKey}/manage")]
        public void GrantManagePermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new GrantManagePermission(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, userKey));
        }
        
        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/view")]
        public void RevokeViewPermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new RevokeViewPermission(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/manage")]
        public void RevokeManagePermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new RevokeManagePermission(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/administrate")]
        public void RevokeAdministratePermission(string organizationKey, string userKey)
        {
            executionEngine.Send(new RevokeAdministratePermission(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, SystemManagers, HttpDelete, Route("organizations/{organizationKey}")]
        public void ArchiveOrganization(string organizationKey)
        {
            executionEngine.Send(new ArchiveOrganization(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey));
        }

        [Secured, SystemManagers, HttpPost, Route("organizations")]
        public string RegisterOrganization(RegisterOrganizationModel model)
        {
            var cmd = new RegisterOrganization(CurrentClaims.ApplicationKey, CurrentClaims.UserId, model.OrganizationKey, model.Name);
            
            cmd.OrganizationKey = cmd.OrganizationKey ?? new Hashids(config.SecretKey).EncodeLong(DateTime.Now.Ticks);
            executionEngine.Send(cmd);
            return cmd.OrganizationKey;
        }
      
        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}")]
        public void UpdateOrganization(string organizationKey, UpdateOrganizationModel model)
        {
            executionEngine.Send(new UpdateOrganization(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, model.Name));
        }
    }
}
