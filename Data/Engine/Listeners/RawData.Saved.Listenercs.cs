﻿using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Data.Domain;
using Trackwane.Data.Events;
using Trackwane.Framework.Common;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using IProvideTransactions = Trackwane.Framework.Interfaces.IProvideTransactions;
using IRepository = Trackwane.Framework.Interfaces.IRepository;

namespace Trackwane.Data.Engine.Listeners
{
    public class RawDataSavedListener : TransactionalListener<SensorReadingSaved>
    {
        public RawDataSavedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(SensorReadingSaved evt, IRepository repository)
        {
            var tracker = repository.Query<Tracker>().SingleOrDefault(x => x.HardwareId == evt.HardwareId);

            if (tracker != null)
            {
                tracker.Update(evt.BatteryLevel, evt.Orientation, evt.Speed, evt.Distance, evt.Coordinates, evt.Heading);

                repository.Persist(tracker);

                return tracker.GetUncommittedChanges();
            }

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
