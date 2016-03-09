using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Data.Domain;
using Trackwane.Data.Engine.Commands;
using Trackwane.Data.Events;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using IProvideTransactions = Trackwane.Framework.Interfaces.IProvideTransactions;
using IRepository = Trackwane.Framework.Interfaces.IRepository;

namespace Trackwane.Data.Engine.Handlers
{
    public class SaveSensorReadingHandler : TransactionalHandler<SaveSensorReading>
    {
        public SaveSensorReadingHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(SaveSensorReading cmd, IRepository repository)
        {
            var deviceData = new SensorReading(cmd.RawDataId, cmd.HardwareId, cmd.Timestamp)
            {
                Coordinates = cmd.Coordinates,
                Orientation = cmd.Orientation,
                BatteryLevel = cmd.BatteryLevel,
                Distance = cmd.Distance,
                Petrol = cmd.Petrol,
                Speed = cmd.Speed,
                Heading = cmd.Heading
            };

            repository.Persist(deviceData);

            return new List<DomainEvent> {new SensorReadingSaved
            {
                HardwareId = deviceData.HardwareId,
                Timestamp = deviceData.Timestamp,
                ReadingKey = deviceData.Key,
                Coordinates = deviceData.Coordinates,
                Orientation = deviceData.Orientation,
                BatteryLevel = deviceData.BatteryLevel,
                Distance = deviceData.Distance,
                Petrol = deviceData.Petrol,
                Speed = deviceData.Speed,
                Heading = deviceData.Heading
            }};
        }
    }
}
