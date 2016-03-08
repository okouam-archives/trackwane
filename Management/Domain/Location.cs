using System;
using Geo.Geometries;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Events;
using Trackwane.Management.Events;

namespace Trackwane.Management.Domain
{
    public class Location : AggregateRoot, IHaveName, IResource
    {
        /* Public */

        public bool IsArchived { get; set; }

        public Point Coordinates { get; set; }

        public string OrganizationKey { get; set; }

        public string Name { get; set; }

        public void Archive()
        {
            Causes(new LocationArchived {
                OrganizationKey = OrganizationKey,
                LocationKey = Key
            });
        }

        public bool IsCloseTo(Point point)
        {
            throw new NotImplementedException();
        }

        public void ChangeName(string newName)
        {
            Causes(new LocationUpdated
            {
                LocationKey = Key,
                OrganizationKey = OrganizationKey,
                Previous = new LocationUpdated.State
                {
                    Name = Name,
                    Coordinates = Coordinates
                },
                Current = new LocationUpdated.State
                {
                    Name = newName,
                    Coordinates = Coordinates
                }
            });
        }

        public void ChangeCoordinates(Point newCoordinates)
        {
            Causes(new LocationUpdated
            {
                LocationKey = Key,
                OrganizationKey = OrganizationKey,
                Previous = new LocationUpdated.State
                {
                    Coordinates = Coordinates,
                    Name = Name
                },
                Current = new LocationUpdated.State
                {
                    Coordinates = newCoordinates,
                    Name = Name
                }
            });
        }

        public Location()
        {
            
        }

        public Location(string id, string OrganizationKey, string name, Point coordinates)
        {
            Causes(new LocationRegistered
            {
                LocationKey = id,
                Name = name,
                OrganizationKey = OrganizationKey,
                Coordinates = coordinates
            });
        }

        /* Protected */

        protected override void Apply(DomainEvent evt)
        {
            When((dynamic)evt);
        }

        /* Private */

        private void When(LocationRegistered evt)
        {
            Key = evt.LocationKey;
            Name = evt.Name;
            OrganizationKey = evt.OrganizationKey;
            Coordinates = evt.Coordinates;
        }

        private void When(LocationArchived evt)
        {
            IsArchived = true;
        }

        private void When(LocationUpdated evt)
        {
            Name = evt.Current.Name;
            Coordinates = evt.Current.Coordinates;
        }
    }
}
