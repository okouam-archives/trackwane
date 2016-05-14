using System.Collections.Generic;
using System.Linq;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Trackers;

namespace Trackwane.Management.Engine.Handlers.Sensors
{
    public class RegisterTrackerHandler : Handler<RegisterSensor>
    {
        public RegisterTrackerHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) :
            base(publisher, transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterSensor cmd, IRepository repository)
        {
            var duplicateCount = repository.Query<Sensor>()
                    .Count(x => x.ApplicationKey == cmd.ApplicationKey 
                        && x.OrganizationKey == cmd.OrganizationId 
                        && x.HardwareId == cmd.HardwareId);

            if (duplicateCount > 0) throw new BusinessRuleException("A duplicate exists");

            var sensor = new Sensor(cmd.ApplicationKey, cmd.TrackerId, cmd.OrganizationId, cmd.HardwareId, cmd.Model, cmd.Identifier);

            repository.Persist(sensor);

            return sensor.GetUncommittedChanges();
        }
    }
}
