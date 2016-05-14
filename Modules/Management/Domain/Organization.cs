using System.Collections.Generic;
using Marten.Schema;
using Trackwane.Framework.Common;

namespace Trackwane.Management.Domain
{
    [DocumentAlias("management_organization")]
    public class Organization : AggregateRoot
    {
        public IList<string> Trackers = new List<string>();

        public IList<string> Drivers = new List<string>();

        public IList<string> Alerts = new List<string>();

        public IList<string> Vehicles = new List<string>();

        public IList<string> Boundaries = new List<string>();

        public IList<string> Locations = new List<string>();

        public Organization(string applicationKey, string organizationKey)
        {
            ApplicationKey = applicationKey;
            Key = organizationKey;
        }

        protected override void Apply(DomainEvent evt)
        {
            throw new System.NotImplementedException();
        }
    }
}
