using StructureMap;

namespace Trackwane.Framework.Interfaces
{
    public interface IServiceLocator<T> where T : Registry, new()
    {
        IContainer GetContainer();
    }
}
