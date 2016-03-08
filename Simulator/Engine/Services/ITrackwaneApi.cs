using System.Collections.Generic;
using Trackwane.Simulator.Domain;

namespace Trackwane.Simulator.Engine.Services
{
    public interface ITrackwaneApi
    {
        IEnumerable<string> SaveRawData(List<GpsEvent> events);
    }
}