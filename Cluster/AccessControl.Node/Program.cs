using Trackwane.Framework.Cluster;

namespace Trackwane.AccessControl.Service
{
    internal static class Program
    {
        private static void Main()
        {
            EntryPoint<Node>.Launch("AccessControl_Engine");
        }
    }
}
