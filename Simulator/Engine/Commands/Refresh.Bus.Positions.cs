using System;
using paramore.brighter.commandprocessor;

namespace Trackwane.Simulator.Engine.Commands
{
    public class RefreshBusPositions : IRequest
    {
        public Guid Id { get; set; }

        public int[] Buses { get; set; }
    }
}
