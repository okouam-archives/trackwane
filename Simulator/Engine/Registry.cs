using Trackwane.Simulator.Engine.Handlers;
using Trackwane.Simulator.Engine.Queries;
using Trackwane.Simulator.Engine.Services;

namespace Trackwane.Simulator.Engine
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            Scan(s =>
            {
                s.SingleImplementationsOfInterface();
                s.WithDefaultConventions();
                s.AssemblyContainingType<RefreshBusPositionsHandler>();
                s.AssemblyContainingType<IConfig>();
                s.AssemblyContainingType<FindVehicles>();
            });

            For<IProvidePositions>().Use<PositionProvider>();
        }
    }
}
