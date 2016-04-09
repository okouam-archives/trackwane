using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Management.Contracts.Events;

namespace Trackwane.Management.Domain
{
    public class Alert : AggregateRoot, IHaveName, IResource
    {
        /* Public */

        public bool IsArchived { get; set; }

        public string Name { get; set; }

        public string OrganizationKey { get; set; }

        public int Threshold { get; internal set; }

        public AlertType Type { get; set; }

        public string Id { get; internal set; }

        protected override void Apply(DomainEvent evt)
        {
            When((dynamic)evt);
        }

        public void Archive()
        {
            Causes(new AlertArchived {
                AlertKey = Key,
                OrganizationKey = OrganizationKey
            });
        }

        /* Internal */

        public Alert()
        {

        }

        public Alert(string id, string name, string OrganizationKey)
        {
            Causes(new AlertCreated
            {
                AlertKey = id,
                Name = name,
                OrganizationKey = OrganizationKey
            });
        }

        public void When(AlertCreated evt)
        {
            Key = evt.AlertKey;
            OrganizationKey = evt.OrganizationKey;
            Threshold = evt.Threshold;
            Type = (AlertType) (int) evt.Type;
        }

        public void When(AlertArchived evt)
        {
            IsArchived = true;
        }

        public void When(AlertEdited evt)
        {
            Name = evt.Current.Name;
            Threshold = evt.Current.Threshold;
            Type = (AlertType)(int) evt.Current.Type;
        }

        public void Edit(string name)
        {
            Causes(new AlertEdited
            {
                OrganizationKey = OrganizationKey,
                AlertKey = Id,
                Previous = new AlertEdited.State
                {
                    Threshold = Threshold,
                    Type = (Contracts.Events.AlertType) (int) Type,
                    Name = Name
                },
                Current = new AlertEdited.State
                {
                    Threshold = Threshold,
                    Type = (Contracts.Events.AlertType)(int)Type,
                    Name = name
                }
            });
        }
    }

    public enum AlertType
    {
        Speed,
        Petrol,
        Battery
    }
}