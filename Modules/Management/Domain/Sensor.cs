using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Management.Contracts.Events;

namespace Trackwane.Management.Domain
{
    public class Sensor : AggregateRoot, IResource
    {
        public bool IsArchived { get; set; }

        public string OrganizationKey { get; set; }

        public string Model { get; internal set; }

        public string HardwareId { get; internal set; }

        public string Identifier { get; set; }
        
        public Sensor()
        {

        }

        public Sensor(string applicationKey, string id, string organizationKey, string hardwareId, string model, string identifier)
        {
            Causes(new SensorRegistered
            {
                ApplicationKey = applicationKey,
                TrackerKey = id,
                HardwareId = hardwareId,
                Model = model,
                OrganizationKey = organizationKey,
                Identifier = identifier
            });
        }
        
        public void Archive()
        {
            Causes(new SensorArchived
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

        private void When(SensorRegistered evt)
        {
            ApplicationKey = evt.ApplicationKey;
            Key = evt.TrackerKey;
            Identifier = evt.Identifier;
            OrganizationKey = evt.OrganizationKey;
            HardwareId = evt.HardwareId;
            Model = evt.Model;
        }

        private void When(SensorArchived evt)
        {
            IsArchived = true;
        }
    }
}
