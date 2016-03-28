using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using PusherServer;
using Trackwane.Data.Domain;
using Trackwane.Data.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using IProvideTransactions = Trackwane.Framework.Interfaces.IProvideTransactions;
using IRepository = Trackwane.Framework.Interfaces.IRepository;

namespace Trackwane.Data.Engine.Listeners
{
    public class TrackerUpdatedListener : TransactionalListener<TrackerUpdated>
    {
        private readonly Config config;

        public TrackerUpdatedListener(IProvideTransactions transaction, 
            IExecutionEngine publisher, ILog log, Config config) : 
            base(transaction, publisher, log)
        {
            this.config = config;
        }

        protected override IEnumerable<DomainEvent> Handle(TrackerUpdated evt, IRepository repository)
        {
            var pusher = new Pusher(config.PusherAppId, config.PusherAppKey, config.PusherAppSecret);

            var tracker = repository.Load<Tracker>(evt.TrackerKey);

            pusher.Trigger(tracker.OrganizationId, "tracker_updated", new
            {
                id = tracker.Key,
                hardwareId = tracker.HardwareId,
                wkt = tracker.Coordinates,
                speed = tracker.Speed,
                orientation = tracker.Orientation,
                heading = tracker.Heading,
                distance = tracker.Distance,
                batteryLevel = tracker.BatteryLevel
            });

            return null;
        }

    }
}
