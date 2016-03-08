using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Events;
using Trackwane.Management.Events;

namespace Trackwane.Management.Domain
{
    public class Driver : AggregateRoot, IHaveName, IResource
    {
        /* Public */

        public bool IsArchived { get; set; }

        public string Name { get; set; }

        public string OrganizationKey { get; set; }

        public void UpdateName(string name)
        {
            if (IsArchived)
            {
                throw new BusinessRuleException();
            }

            Causes(new DriverUpdated
            {
                OrganizationKey = OrganizationKey,
                DriverKey = Key,
                Previous = new DriverUpdated.State
                {
                    Name = Name
                },
                Current = new DriverUpdated.State
                {
                    Name = name
                }
            });
        }

        public Driver()
        {
            
        }

        public Driver(string key, string OrganizationKey, string name)
        {
            Causes(new DriverRegistered
            {
                DriverKey = key,
                OrganizationKey = OrganizationKey,
                Name = name
            });
        }

        public void Archive()
        {
            if (!IsArchived)
            {
                Causes(new DriverArchived
                {
                    DriverKey = Key,
                    OrganizationKey = OrganizationKey
                });
            }
        }
        
        /* Protected */
        protected override void Apply(DomainEvent evt)
        {
            When((dynamic)evt);
        }

        /* Private */

        private void When(DriverRegistered evt)
        {
            Key = evt.DriverKey;
            Name = evt.Name;
            OrganizationKey = evt.OrganizationKey;
        }

        private void When(DriverUpdated evt)
        {
            if (evt.Current.Name != null)
            {
                Name = evt.Current.Name;
            }
        }
        
        private void When(DriverArchived evt)
        {
            IsArchived = true;
        }
    }
}
