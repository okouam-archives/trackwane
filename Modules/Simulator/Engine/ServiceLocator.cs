using StructureMap;

namespace Trackwane.Simulator.Engine
{
   public  class ServiceLocator
    {
        public static IContainer Use()
        {
            var container = new Container();

            container.Configure(x =>
            {
                x.AddRegistry<Registry>();
            });

            return container;
        }
    }
}
