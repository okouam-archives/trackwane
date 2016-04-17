using System;
using Marten;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Management.Contracts.Models;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Queries.Drivers
{
    public class FindById : Query<DriverDetails>, IOrganizationQuery
    {
        public DriverDetails Execute(string driverId)
        {
            return Execute(repository =>
            {
                if (string.IsNullOrEmpty(driverId))
                {
                    throw new Exception("Invalid driver ID");
                }

                var driver = repository.Find<Driver>(driverId, ApplicationKey);

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
