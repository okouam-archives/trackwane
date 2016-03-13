using System.Linq;
using NUnit.Framework;
using Shouldly;
using Trackwane.Simulator.Engine.Queries;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Tests.Behaviors.Engine.Queries
{
    internal class FindVehiclePositionsTests
    {
        [Test]
        public void When_Vehicles_Exist_Finds_Their_Positions()
        {
            var query = new GetVehicleReadings(new ReadingProvider(new Config()), new FindVehicles(new ReadingProvider(new Config()), new GetRoutes(new Config())));
            var positions = query.Execute(1004, 1006, 1009);
            positions.Count().ShouldBe(3);
        }
    }
}
