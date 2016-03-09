using System.Collections.Generic;
using Trackwane.Framework.Common;

namespace Trackwane.Management.Domain
{
    public class Organization : AggregateRoot
    {
        public IList<string> Trackers = new List<string>();

        public IList<string> Drivers = new List<string>();

        public IList<string> Alerts = new List<string>();

        public IList<string> Vehicles = new List<string>();

        public IList<string> Boundaries = new List<string>();

        public IList<string> Locations = new List<string>();

        public Organization(string organizationKey)
        {
            Key = organizationKey;
        }

        protected override void Apply(DomainEvent evt)
        {
            throw new System.NotImplementedException();
        }
    }
}
