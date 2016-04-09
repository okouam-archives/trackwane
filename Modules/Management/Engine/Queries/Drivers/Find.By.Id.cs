using System;
using Raven.Client;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Drivers
{
    public class FindById : Query<DriverDetails>, IScopedQuery
    {
        public DriverDetails Execute(string driverId)
        {
            return Execute(repository =>
            {
                if (string.IsNullOrEmpty(driverId))
                {
                    throw new Exception("Invalid driver ID");
                }

                var driver = repository.Load<Driver>(driverId);

                if (driver == null)
                {
                    throw new BusinessRuleException("Unknown driver ID");
                }

                return new DriverDetails {
                    Name = driver.Name,
                    IsArchived = driver.IsArchived,
                    Id = driver.Key
                };
            });
        }

        public FindById(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public string OrganizationKey { get; set; }
    }
}
