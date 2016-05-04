using System.Collections.Generic;
using System.Linq;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Listeners.Vehicles
{
    public class DriverArchivedListener : TransactionalListener<DriverArchived>
    {
        public DriverArchivedListener(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(DriverArchived evt, IRepository repository)
        {
            var driver = repository.Find<Driver>(evt.DriverKey, evt.ApplicationKey);

            var vehicles = repository.Query<Vehicle>()
                .Where(x => x.DriverId == evt.DriverKey);

            foreach (var vehicle in vehicles)
            {
                vehicle.UnassignDriver(driver);

                foreach (var change in vehicle.GetUncommittedChanges())
                {
                    yield return change;
                }
            }
        }
    }
}
