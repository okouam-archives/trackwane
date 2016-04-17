using NUnit.Framework;
using Trackwane.Data.Shared.Client;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Simulator.Engine.Commands;
using Trackwane.Simulator.Engine.Handlers;
using Trackwane.Simulator.Engine.Queries;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Tests.Behaviors.Engine.Handlers
{
    internal class RefreshBusPositionsHandlerTests
    {
        [Test]
        public void Finds_Positions_For_Given_Vehicles()
        {
            var positionProvider = new ReadingProvider(new Simulator.Engine.Services.Config());
            var dataContext = new DataContext("http://localhost:37470", new PlatformConfig()).UseWithoutAuthentication();
            var handler = new SimulateSensorReadingsHandler(dataContext, new GetVehicleReadings(positionProvider, new FindVehicles(positionProvider, new GetRoutes(new Simulator.Engine.Services.Config()))));

            handler.Handle(new SimulateSensorReadings
            {
                Buses = new[] { 1004, 1006, 1009 }
            });
        }
    }
}
