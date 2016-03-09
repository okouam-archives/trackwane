using System;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Management.Events;

namespace Trackwane.Management.Domain
{
    public class Vehicle : AggregateRoot, IResource
    {
        /* Public */

        public bool IsArchived { get; set; }

        public string OrganizationKey { get; set; }

        public string DriverId { get; set; }

        public string TrackerId { get; set; }

        public string Identifier { get; set; }

        public Vehicle()
        {
        }

        public Vehicle(string vehicleId, string OrganizationKey, string identifier)
        {
            Causes(new VehicleRegistered
            {
                Identifier = identifier,
                VehicleKey = vehicleId,
                OrganizationKey = OrganizationKey
            });
        }
        
        public void Archive()
        {
            Causes(new VehicleArchived
            {
                OrganizationKey = OrganizationKey,
                VehicleKey = Key
            });
        }

        public void UnassignDriver(Driver driver)
        {
            throw new NotImplementedException();
        }

        public void UnassignTracker(string trackerId)
        {
            throw new NotImplementedException();
        }

        public void AssignDriver(Driver driver)
        {
            if (driver.IsArchived) throw new BusinessRuleException();

            if (IsArchived) throw new BusinessRuleException();

            if (driver.OrganizationKey != OrganizationKey) throw new BusinessRuleException();

            Causes(new VehicleAssignedDriver
            {
                VehicleKey = Key,
                DriverKey = driver.Key
            });
        }

        public void AssignTracker(string trackerId)
        {
            if (IsArchived) throw new BusinessRuleException();

            Causes(new VehicleAssignedTracker()
            {
                OrganizationKey = OrganizationKey,
                TrackerKey = trackerId,
                VehicleKey = Key
            });
        }

        public void Update(string newIdentifier)
        {
            if (!string.IsNullOrWhiteSpace(newIdentifier))
            {
                Causes(new VehicleUpdated
                {
                    VehicleKey = Key,
                    OrganizationKey = OrganizationKey,
                    Previous = new VehicleUpdated.State
                    {
                       Identifier = Identifier
                    },
                    Current = new VehicleUpdated.State
                    {
                        Identifier = newIdentifier
                    }
                });
            }
        }

        /* Protected */

        protected override void Apply(DomainEvent evt)
        {
            When((dynamic)evt);
        }

        /* Private */

        private void When(VehicleRegistered evt)
        {
            Key = evt.VehicleKey;
            Identifier = evt.Identifier;
            OrganizationKey = evt.OrganizationKey;
        }

        private void When(VehicleAssignedDriver evt)
        {
            DriverId = evt.DriverKey;
        }

        private void When(VehicleAssignedTracker evt)
        {
            TrackerId = evt.TrackerKey;
        }

        private void When(VehicleArchived evt)
        {
            IsArchived = true;
        }

        private void When(VehicleUpdated evt)
        {
            Identifier = evt.Current.Identifier;
        }
    }
}
