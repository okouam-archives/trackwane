using System.Collections.Generic;
using Trackwane.Simulator.Domain;

namespace Trackwane.Simulator.Engine.Services
{
    public interface IProvideReadings
    {
        IEnumerable<Position> Retrieve(string type, params string[] items);
    }
}