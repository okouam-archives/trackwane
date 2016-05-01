using System.Linq;
using NUnit.Framework;
using Shouldly;
using Trackwane.Simulator.Engine.Queries;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Tests.Behaviors.Engine.Queries
{
    internal class FindVehiclesTests
    {
        [Test]
        public void Retrieves_Vehicles_For_Routes()
        {
            var query = new FindVehicles(new ReadingProvider(new Config()), new GetRoutes(new Config()));
            var vehicles = query.Execute().ToList();
            vehicles.Count.ShouldBeGreaterThan(0);
        }
    }
}
