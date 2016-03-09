using System;
using paramore.brighter.commandprocessor;

namespace Trackwane.Simulator.Engine.Commands
{
    public class SimulateSensorReadings : IRequest
    {
        public Guid Id { get; set; }

        public int[] Buses { get; set; }
    }
}
