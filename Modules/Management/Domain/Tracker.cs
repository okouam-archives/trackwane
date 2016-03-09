using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Management.Events;

namespace Trackwane.Management.Domain
{
    public class Tracker : AggregateRoot, IResource
    {
        public bool IsArchived { get; set; }

        public string OrganizationKey { get; set; }

        public string Model { get; internal set; }

        public string HardwareId { get; internal set; }
        
        public Tracker()
        {

        }

        public Tracker(string id, string OrganizationKey, string hardwareId, string model)
        {
            Causes(new TrackerRegistered
            {
                TrackerKey = id,
                HardwareId = hardwareId,
                Model = model,
                OrganizationKey = OrganizationKey
            });
        }
        
        public void Archive()
        {
            Causes(new TrackerArchived
            {
                TrackerKey = Key
            });
        }

        /* Protected */

        protected override void Apply(DomainEvent evt)
        {
            When((dynamic) evt);
        }

        /* Private */

        private void When(TrackerRegistered evt)
        {
            Key = evt.TrackerKey;
            OrganizationKey = evt.OrganizationKey;
            HardwareId = evt.HardwareId;
            Model = evt.Model;
        }

        private void When(TrackerArchived evt)
        {
            IsArchived = true;
        }
    }
}
