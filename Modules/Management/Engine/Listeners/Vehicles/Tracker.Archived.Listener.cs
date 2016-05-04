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
    internal class TrackerArchivedListener : TransactionalListener<TrackerArchived>
    {
        public TrackerArchivedListener(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(TrackerArchived evt, IRepository repository)
        {
            var tracker = repository.Find<Tracker>(evt.TrackerKey, evt.ApplicationKey);

            var vehicles = repository.Query<Vehicle>()
                .Where(x => x.DriverId == evt.TrackerKey);

            foreach (var vehicle in vehicles)
            {
                vehicle.UnassignTracker(tracker.Key);

                foreach (var change in vehicle.GetUncommittedChanges())
                {
                    yield return change;
                }
            }
        }
    }
}
