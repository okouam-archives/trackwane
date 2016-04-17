﻿using System;
using System.Web.Http;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Web.Security;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Engine.Commands.Boundaries;
using Trackwane.Management.Engine.Queries.Boundaries;

namespace Trackwane.Management.Engine.Controllers
{
    [RoutePrefix("organizations/{organizationKey}")]
    public class BoundaryApiController : BaseManagementController
    {
        private const string RESOURCE_URL = "boundaries/{key}";
        private const string COLLECTION_URL = "boundaries";

        public BoundaryApiController(IExecutionEngine dispatcher) : base(dispatcher)
        {
        }

        [Secured, Managers, HttpPost, Route(RESOURCE_URL)]
        public void UpdateBoundary(string organizationKey, string key, UpdateBoundaryModel model)
        {
            dispatcher.Send(new UpdateBoundary(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, key)
            {
                Coordinates = model.Coordinates,
                Name = model.Name
            });
        }

        [Secured, Viewers, HttpGet, Route(RESOURCE_URL)]
        public BoundaryDetails FindById(string organizationKey, string id)
        {
            return dispatcher.Query<FindById>(organizationKey).Execute(id);
        }

        [Secured, Viewers, HttpGet, Route(COLLECTION_URL)]
        public ResponsePage<BoundarySummary> FindBySearchCriteria(string organizationKey, string name)
        {
            return dispatcher.Query<FindBySearchCriteria>(organizationKey).Execute(name);
        }

        [Secured, Managers, HttpDelete, Route(RESOURCE_URL)]
        public void ArchiveBoundary(string organizationKey, string key)
        {
            dispatcher.Send(new ArchiveBoundary(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, key));
        }

        [Secured, Managers, HttpPost, Route(COLLECTION_URL)]
        public void CreateBoundary(string organizationKey, CreateBoundaryModel model)
        {
            dispatcher.Send(new CreateBoundary(CurrentClaims.ApplicationKey, CurrentClaims.UserId, organizationKey, model.Name, model.Coordinates,
                (BoundaryType) Enum.Parse(typeof (BoundaryType), model.Type), model.Key));
        }
    }
}
