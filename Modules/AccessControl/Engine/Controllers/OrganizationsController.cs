//using System;

using System;
using System.Collections.Generic;
using System.Web.Http;
using HashidsNet;
using Trackwane.AccessControl.Contracts.Contracts;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.AccessControl.Engine.Queries.Organizations;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Controllers
{
    public class OrganizationsController : BaseApiController
    {
        private readonly IExecutionEngine executionEngine;
        private readonly IPlatformConfig config;

        public OrganizationsController(IExecutionEngine executionEngine, IPlatformConfig config)
        {
            this.executionEngine = executionEngine;
            this.config = config;
        }

        [Secured, Administrators, HttpGet, Route("organizations/{organizationKey}")]
        public OrganizationDetailsResponse FindById(string organizationKey)
        {
            return executionEngine.Query<FindByKey>(AppKeyFromHeader, organizationKey).Execute();
        }

        [Secured, SystemManagers, HttpGet, Route("organizations")]
        public List<OrganizationDetailsResponse> Find()
        {
            return executionEngine.Query<Find>(AppKeyFromHeader).Execute();
        }

        [Secured, Administrators, HttpGet, Route("organizations/count")]
        public int Count()
        {
            return executionEngine.Query<Count>(AppKeyFromHeader).Execute();
        }

        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users/{userKey}/view")]
        public void GrantViewPermission(string organizationKey, string userKey)
        {
            executionEngine.Handle(new GrantViewPermission(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey));
        }
           
        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}/users/{userKey}/manage")]
        public void GrantManagePermission(string organizationKey, string userKey)
        {
            executionEngine.Handle(new GrantManagePermission(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey));
        }
        
        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/view")]
        public void RevokeViewPermission(string organizationKey, string userKey)
        {
            executionEngine.Handle(new RevokeViewPermission(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/manage")]
        public void RevokeManagePermission(string organizationKey, string userKey)
        {
            executionEngine.Handle(new RevokeManagePermission(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, Administrators, HttpDelete, Route("organizations/{organizationKey}/users/{userKey}/administrate")]
        public void RevokeAdministratePermission(string organizationKey, string userKey)
        {
            executionEngine.Handle(new RevokeAdministratePermission(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, userKey));
        }

        [Secured, SystemManagers, HttpDelete, Route("organizations/{organizationKey}")]
        public void ArchiveOrganization(string organizationKey)
        {
            executionEngine.Handle(new ArchiveOrganization(AppKeyFromHeader, CurrentClaims.UserId, organizationKey));
        }

        [Secured, SystemManagers, HttpPost, Route("organizations")]
        public string RegisterOrganization(RegisterOrganizationRequest request)
        {
            var cmd = new RegisterOrganization(AppKeyFromHeader, CurrentClaims.UserId, request.OrganizationKey, request.Name);
            
            cmd.OrganizationKey = cmd.OrganizationKey ?? new Hashids(config.SecretKey).EncodeLong(DateTime.Now.Ticks);
            executionEngine.Handle(cmd);
            return cmd.OrganizationKey;
        }
      
        [Secured, Administrators, HttpPost, Route("organizations/{organizationKey}")]
        public void UpdateOrganization(string organizationKey, UpdateOrganizationRequest request)
        {
            executionEngine.Handle(new UpdateOrganization(AppKeyFromHeader, CurrentClaims.UserId, organizationKey, request.Name));
        }
    }
}
